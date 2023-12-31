#include <Servo.h>
#include <Dynamixel2Arduino.h>

// 초음파를 사용하기 위한 아두이노 핀 위치 지정
#define echo 47
#define trig 49
#define echo_2 41
#define trig_2 43
#include <DFMobile.h>
#include <DFRobot_HuskyLens.h>
#include <HUSKYLENS.h>
#include <HUSKYLENSMindPlus.h>
#include <HuskyLensProtocolCore.h>
#include <PIDLoop.h>
HUSKYLENS huskylens;

void printResult(HUSKYLENSResult result);

// Please modify it to suit your hardware.
#if defined(ARDUINO_AVR_UNO) || defined(ARDUINO_AVR_MEGA2560)
#include <SoftwareSerial.h>
SoftwareSerial soft_serial(7, 8);
#define DXL_SERIAL Serial
#define DEBUG_SERIAL soft_serial
const int DXL_DIR_PIN = 2;
#elif defined(ARDUINO_SAM_DUE)
#define DXL_SERIAL Serial
#define DEBUG_SERIAL SerialUSB
const int DXL_DIR_PIN = 2;
#elif defined(ARDUINO_SAM_ZERO)
#define DXL_SERIAL Serial1
#define DEBUG_SERIAL SerialUSB
const int DXL_DIR_PIN = 2;
#elif defined(ARDUINO_OpenCM904)
#define DXL_SERIAL Serial3
#define DEBUG_SERIAL Serial
const int DXL_DIR_PIN = 22;
#elif defined(ARDUINO_OpenCR)
#define DXL_SERIAL Serial3
#define DEBUG_SERIAL Serial
const int DXL_DIR_PIN = 84;
#elif defined(ARDUINO_OpenRB)
#define DXL_SERIAL Serial1
#define DEBUG_SERIAL Serial
const int DXL_DIR_PIN = -1;
#else
#define DXL_SERIAL Serial1
#define DEBUG_SERIAL Serial
const int DXL_DIR_PIN = 2;
#endif

const float DXL_PROTOCOL_VERSION = 1.0;

Dynamixel2Arduino dxl(DXL_SERIAL, DXL_DIR_PIN);
using namespace ControlTableItem;

//초음파 센서를 측정하기 위한 함수
int ultrasonic() {
  //초음파을 일정시간동안 발사
  digitalWrite(trig, LOW);
  delayMicroseconds(2);
  digitalWrite(trig, HIGH);
  delayMicroseconds(2);
  digitalWrite(trig, LOW);

  //그 후 거리 식으로 나온 값을 return
  return pulseIn(echo, HIGH) * 17 / 1000;
}

//뒤에있는 초음파 센서를 측정하기 위한 함수
int ultrasonic_back() {

  digitalWrite(trig_2, LOW);
  delayMicroseconds(2);
  digitalWrite(trig_2, HIGH);
  delayMicroseconds(2);
  digitalWrite(trig_2, LOW);


  return pulseIn(echo_2, HIGH) * 17 / 1000;
}

//모터를 속도모드로 변경
void setting_OP_VELOCITY(int port) {
  //다이나믹셀 함수로 받은 AX-12A모터ID를 속도 모드로 변경
  dxl.ping(port);
  dxl.torqueOff(port);
  //AX-12모터를 OFF시킴
  dxl.setOperatingMode(port, OP_VELOCITY);
  //AX-12모터에 모드를 속도 모드로 변경함
  dxl.torqueOn(port);
  //AX-12모터를 ON
}

//모터를 포지션 모드로 변경
void setting_OP_POSITION(int port) {
  //다이나믹셀 함수로 받은 AX-12A모터ID를 포지션 모드로 변경
  dxl.ping(port);
  dxl.torqueOff(port);
  //AX-12모터를 OFF시킴
  dxl.setOperatingMode(port, OP_POSITION);
  //AX-12모터에 모드를 속도 모드로 변경함
  dxl.torqueOn(port);
  //AX-12모터를 다시 ON
}
//port는 구동부에 축을 담당하는 모터의 ID를 말한다.
//target_dergee는 port모터가 목표로 하는 각을 저장한다.
//axis_motor_speed는 port모터가 목표로 하는 속도를 저장한다.
//port_2는 구동부 바퀴를 담당하는 모터를 뜻 한다.
//target_dergee_2는 port_2가 목표로 하는 스피드를 말한다.
void motor_axis(int port, int target_degree,int axis_motor_speed,int port_2, int target_speed) {
  //모터의 시작 상태를 false로 설정한다
  bool start_motor_check = false;
  //각각 입력받은 값을 설정하여 작동한다.
  dxl.setGoalPosition(port, target_degree, UNIT_DEGREE);
  dxl.setGoalVelocity(port, axis_motor_speed);
  dxl.setGoalVelocity(port_2, target_speed);
  DEBUG_SERIAL.print("first position : ");
  //시작 모터 각도를 출력한다
  DEBUG_SERIAL.println(dxl.getPresentPosition(port, UNIT_DEGREE));
  while (start_motor_check != true) {
    //목표 각도에 도달하면 delay(10) 후에 현 상태를 유지하고 있는 지 확인한다.
    if (target_degree-3 <= dxl.getPresentPosition(port, UNIT_DEGREE) && dxl.getPresentPosition(port, UNIT_DEGREE) <= target_degree+3) {
      delay(10);
      if (target_degree-3 <= dxl.getPresentPosition(port, UNIT_DEGREE) && dxl.getPresentPosition(port, UNIT_DEGREE) <= target_degree+3) {
        //그 후 while문을 탈출하기 위하 true로 변경하고 while문을 탈출한다.
        start_motor_check = true;
        DEBUG_SERIAL.println("check");
      }
    }
    //그 후 완료를 알린다.
    DEBUG_SERIAL.println("position check");
    DEBUG_SERIAL.println(dxl.getPresentPosition(6, UNIT_DEGREE));
    delay(50);
  }
}

void setup() {

  //블루투스 통신을 보드 통신 속도
  Serial1.begin(9600);
  Serial2.begin(9600);
  //다이나믹 셀 쉴드를 통한 Serial 보드 통신 속도
  DEBUG_SERIAL.begin(115200);
  dxl.begin(1000000);
  dxl.setPortProtocolVersion(DXL_PROTOCOL_VERSION);
  Wire.begin();

  pinMode(echo, INPUT);
  pinMode(trig, OUTPUT);
  pinMode(echo_2, INPUT);
  pinMode(trig_2, OUTPUT);

  DEBUG_SERIAL.println("check");
  //허스키 렌즈의 연결상태를 확인
  while (!huskylens.begin(Wire)) {
    DEBUG_SERIAL.println(F("Begin failed!"));
    DEBUG_SERIAL.println(F("1.Please recheck the \"Protocol Type\" in HUSKYLENS (General Settings>>Protocol Type>>I2C)"));
    DEBUG_SERIAL.println(F("2.Please recheck the connection."));
    delay(10);
  }
  DEBUG_SERIAL.println("check");

  //사용할 모터의 포지션 설정
  setting_OP_POSITION(2);
  setting_OP_POSITION(6);
  setting_OP_VELOCITY(4);
  setting_OP_VELOCITY(18);
  static long int time_check = 0;
  DEBUG_SERIAL.println("psition...");
  Serial.println("psition...");
  dxl.setGoalVelocity(2, 200);
  dxl.setGoalVelocity(6, 200);
  dxl.setGoalPosition(2, 150, UNIT_DEGREE);
  dxl.setGoalPosition(6, 150, UNIT_DEGREE);
  delay(5000);
  Serial.println("cm");
}

int save_check_id = 0;
int count = 0;
bool end = 0;
bool move_end_check = 0;
int check_id[10];
void loop() {


  //---------------------------------------------------------------------------------------------------
  //------------------------------------------처음 이동 시작--------------------------------------------
  //---------------------------------------------------------------------------------------------------
  //처음에 위로 올라가는 위치를 잡는 코드 미완성

  static bool right_setting_check = 0;
  static bool left_setting_check = 0;
  bool start_time_check = 0;
  long long int start_time = 0;
  int set_degree = 60;

  delay(1000);
  //로봇의 앞쪽 부분을 꺽어 바코드 쪽으로 갈 준비를 한다.
  //모터가 간혹 값이 튀어 위치에 도달했다고 인식하는 경우가 있다. 이를 해결하기 위해 도달할 경우 그 지점을 저장하고 10ms 뒤에 저장한 값과 현재 값을 비교하여 동일한 위치인지 여부를 물어 본다.
  motor_axis(6,60,40,18,1124);
  dxl.setGoalVelocity(18, 0);
  delay(2000);



  //로봇의 뒷쪽 부분을 꺽어 바코드 쪽으로 갈 준비를 한다.
  motor_axis(2,60,40,4,1204);

  dxl.setGoalVelocity(4, 0);
  delay(2000);
  static bool start_check = 0;

  //허스키 렌즈가 레일 앞 태그를 읽을때 까지 앞으로 이동해 읽는 경우 멈춘다.
  while (start_check != 1) {
    dxl.setGoalVelocity(4, 120);
    dxl.setGoalVelocity(18, 1144);
    if (!huskylens.request()) {
      DEBUG_SERIAL.println(F("허스키 렌즈 연결 불가"));
    } else if (!huskylens.isLearned()) {
      DEBUG_SERIAL.println(F("학습데이터 없음"));
    } else if (!huskylens.available()) {
      DEBUG_SERIAL.println(F("태그 없음"));
    } else {
      while (huskylens.available()) {
        HUSKYLENSResult result = huskylens.read();
        if (result.xCenter <= 160) {
          if (result.ID == 7) {
            DEBUG_SERIAL.println("stop");
            dxl.setGoalVelocity(4, 0);
            dxl.setGoalVelocity(18, 0);
            start_check = 1;
          }
        }
      }
    }
  }
  delay(1000);

  //레일 앞에서 앞으로 이동하기 위해 앞바퀴를 레일에 맞추어 들어갈 준비를 한다.
  motor_axis(6,150,40,18,100);

  dxl.setGoalVelocity(18, 0);
  delay(2000);

  //레일 앞에서 앞으로 이동하기 위해 뒷바퀴를 레일에 맞추어 들어갈 준비를 한다.
  motor_axis(2,150,40,4,100);

  dxl.setGoalVelocity(4, 0);
  delay(2000);

  // 허스키 렌즈가 ID1번을 확인할때 까지 앞으로 이동
  start_check = 0;
  while (start_check != 1) {
    dxl.setGoalVelocity(4, 400);
    dxl.setGoalVelocity(18, 1424);
    if (!huskylens.request()) {
      DEBUG_SERIAL.println(F("허스키 렌즈 연결 불가"));
    } else if (!huskylens.isLearned()) {
      DEBUG_SERIAL.println(F("학습데이터 없음"));
    } else if (!huskylens.available()) {
      DEBUG_SERIAL.println(F("태그 없음"));
    } else {
      while (huskylens.available()) {
        HUSKYLENSResult result = huskylens.read();
        if (result.yCenter <= 120) {
          if (result.ID == 1) {
            DEBUG_SERIAL.println("stop");
            dxl.setGoalVelocity(4, 0);
            dxl.setGoalVelocity(18, 0);
            start_check = 1;
          }
        }
      }
    }
  }
  //----------------------------------------------------------------------------------------------------
  //------------------------------------------처음 이동 끝----------------------------------------------
  //----------------------------------------------------------------------------------------------------

  start_check = 0;
  while (move_end_check != 1) {

    static int text;
    static long int time = 0;
    float motor_speed = 700;
    static bool end_ = 0;
    static float cm;
    static bool find = 0;
    static int save_ID = 6;
    static int save_speed_ID = 1;
    static int save_stop_ID = 7;

    static long int time_check = 0;

    if (find != 1) {
      if (start_check == 0) {
        DEBUG_SERIAL.println("in start move");
        time_check = millis();
        start_check = 1;
      }
      if (millis() - time >= 500) {
        cm = ultrasonic();
        time = millis();
        DEBUG_SERIAL.print("cm : ");
        DEBUG_SERIAL.println(cm);
        DEBUG_SERIAL.print(Serial2.read());
        DEBUG_SERIAL.print(dxl.getPresentVelocity(4, UNIT_RPM));
        DEBUG_SERIAL.print(",");
        DEBUG_SERIAL.print(dxl.getPresentVelocity(18, UNIT_RPM));
        DEBUG_SERIAL.print(",");
        DEBUG_SERIAL.println(motor_speed);
      }

      if (millis() - time_check >= 16000) {
        if (1.1 <= cm && cm <= 4) {

          //위 판에 인식시 작동함
          dxl.setGoalVelocity(4, 0);
          dxl.setGoalVelocity(18, 0);
          DEBUG_SERIAL.println("stop");
          while (cm <= 7) {
            //cm 가 6 가 될때까지 작동 밑으로 이동
            DEBUG_SERIAL.println("in");
            cm = ultrasonic();
            DEBUG_SERIAL.println(cm);
            dxl.setGoalVelocity(4, 1224);
            dxl.setGoalVelocity(18, 200);
          }
          //위 조건이 끝나는 즉시 모터의 움직임 종료(로테이션 모드의 특징)
          dxl.setGoalVelocity(4, 0);
          dxl.setGoalVelocity(18, 0);
          delay(1000);
          setting_OP_POSITION(2);
          setting_OP_POSITION(6);
          DEBUG_SERIAL.println("trun");
          dxl.setGoalVelocity(2, 500);
          dxl.setGoalPosition(2, 239, UNIT_DEGREE);
          //갑작이 소비 전력이 높아지면 갑작이 꺼진다 이를 방지하기 위해서 하나씩 움직인다
          delay(1000);
          dxl.setGoalVelocity(6, 500);
          dxl.setGoalPosition(6, 244, UNIT_DEGREE);
          delay(2000);
          DEBUG_SERIAL.println("start");
          bool huskylens_check = 0;
          while (huskylens_check != 1) {
            dxl.setGoalVelocity(4, motor_speed);
            dxl.setGoalVelocity(18, motor_speed + 1024);
            //허스키 렌즈의 연결 상태 확인
            if (!huskylens.request()) {
              DEBUG_SERIAL.println(F("허스키 렌즈 연결 불가"));
            } else if (!huskylens.isLearned()) {
              DEBUG_SERIAL.println(F("학습데이터 없음"));
            } else if (!huskylens.available()) {
              DEBUG_SERIAL.println(F("태그 없음"));
            } else {
              //허스키 렌즈의 상태를 지속해서 확인하고 값의 입력이 있는 경우 이후 해당 불러온 값에 맞는 조건식을 출력
              while (huskylens.available()) {
                HUSKYLENSResult result = huskylens.read();
                DEBUG_SERIAL.print("X : ");
                DEBUG_SERIAL.print(result.xCenter);
                DEBUG_SERIAL.print(", Y : ");
                DEBUG_SERIAL.println(result.yCenter);
                //상자의 ID와 입력된 ID값이 맞는 지 확인후 맞는 경우 태그의 좌표를 확인 x가 130이상 190이하인 경우 멈춤
                if (result.ID == save_ID) {
                  if (130 <= result.xCenter && result.xCenter <= 190) {
                    DEBUG_SERIAL.println(result.ID);
                    dxl.setGoalVelocity(4, 0);
                    dxl.setGoalVelocity(18, 0);
                    huskylens_check = 1;
                  }
                }
              }
            }
          }
          delay(3000);
          find = 1;
          // 오른쪽 끝 판의 거리를 초음파로 인식하여 82cm 이상인 경우 까지 계속 움직임
          while (cm <= 81) {
            cm = ultrasonic();
            DEBUG_SERIAL.print("cm : ");
            DEBUG_SERIAL.println(cm);
            dxl.setGoalVelocity(4, 1624);
            dxl.setGoalVelocity(18, 600);
          }
          dxl.setGoalVelocity(4, 0);
          dxl.setGoalVelocity(18, 0);
          bool back_cm_check = 0;
          //뒷바퀴의 위치가 맞지 않을 경우 이를 실행(오류가 많이 보류)

          //해당 조건문을 나온후 모터를 즉시 종료
          dxl.setGoalVelocity(4, 0);
          dxl.setGoalVelocity(18, 0);
          delay(1000);
          //2,6번 모터를 150에 맞추어 다시 트랙을 나올 준비를 함
          dxl.setGoalVelocity(2, 700);
          dxl.setGoalPosition(2, 150, UNIT_DEGREE);

          delay(1500);
          dxl.setGoalVelocity(6, 700);
          dxl.setGoalPosition(6, 150, UNIT_DEGREE);

          delay(3000);
          DEBUG_SERIAL.println("start");
          //첫 시작에 부분을 알려주는 bool값
          bool start_back = 0;
          long int start_back_time = 0;
          //허스키 랜즈가 보기 전까지를 상태를 저장하기 위해
          huskylens_check = 0;
          motor_speed = 500;

          bool speed_control = 0;
          while (huskylens_check != 1) {
            cm = ultrasonic();
            DEBUG_SERIAL.print("cm : ");
            DEBUG_SERIAL.println(cm);
            dxl.setGoalVelocity(4, 1624);
            dxl.setGoalVelocity(18, 600);

            //해당 조건문은 프로그램이 사작되면 최초 1회만 millis를 start_back_time에 저장한다. 그 후 다시 실행을 막기 위해 start_back 을 1(true)로 바꾸어 준다
            if (start_back == false) {
              start_back_time = millis();
              start_back = 1;
            }



            if (28 <= cm && cm <= 32) {
              /*
          뒷 바퀴가 곡선을 처음 만난 시점부터 뒷 바퀴의 속도를 증가
          */
              dxl.setGoalVelocity(4, motor_speed + 1024);
              dxl.setGoalVelocity(18, motor_speed + 200);
              DEBUG_SERIAL.println("a");
            } else if (5 <= cm && cm <= 9) {
              if (millis() - start_back_time >= 2000) {
                DEBUG_SERIAL.println("b");
                dxl.setGoalVelocity(4, motor_speed + 1324);
                dxl.setGoalVelocity(18, motor_speed);
              }
            } else {
              DEBUG_SERIAL.println("c");
              /*
          장치에 해당 사항이 없으면 트렉을 계속 이동
          */
              dxl.setGoalVelocity(4, motor_speed + 1024);
              dxl.setGoalVelocity(18, motor_speed);
            }
            // 지면의 도착을 알리는 태그를 확인하는 부분
            if (!huskylens.request()) {
              DEBUG_SERIAL.println(F("허스키 렌즈 연결 불가"));
            } else if (!huskylens.isLearned()) {
              DEBUG_SERIAL.println(F("학습데이터 없음"));
            } else if (!huskylens.available()) {
              DEBUG_SERIAL.println(F("태그 없음"));
            } else {
              while (huskylens.available()) {
                //허스키 렌즈가 받고 있는 태그의 위치를 지속적으로 DEBUG_SERIAL을 통해서 출력
                HUSKYLENSResult result = huskylens.read();
                DEBUG_SERIAL.print("X : ");
                DEBUG_SERIAL.print(result.xCenter);
                DEBUG_SERIAL.print(", Y : ");
                DEBUG_SERIAL.println(result.yCenter);
                //지면에 6x6 태그를 인식
                if (result.ID == save_stop_ID) {
                  if (120 <= result.yCenter && result.yCenter <= 160) {
                    //6x6태그를 인식하고 중간에서 정지하고 while문을 빠져나가는 코드
                    DEBUG_SERIAL.println(result.ID);
                    dxl.setGoalVelocity(4, 0);
                    dxl.setGoalVelocity(18, 0);
                    huskylens_check = 1;
                  }
                } else if (result.ID == save_speed_ID && speed_control == false) {
                  //지면에 1x1을 보는 경우 속도를 조종하는 태그를 인식하고 모터의 속도를 조종
                  speed_control = true;
                  motor_speed = 150;
                  dxl.setGoalVelocity(4, 0);
                  dxl.setGoalVelocity(18, 0);
                  dxl.setGoalPosition(2, 150, UNIT_DEGREE);
                  dxl.setGoalPosition(6, 150, UNIT_DEGREE);
                  delay(5000);
                  DEBUG_SERIAL.println("중간 포지션 조종 끝");
                }
              }
            }
          }

          //각각 모터의 각도를 옆으로 이동하기 위해 세팅하는 코드

          delay(1000);
          motor_axis(6, 60, 1074, 18, 1224);

          start_time_check = false;
          set_degree = 60;

          delay(1000);
          motor_axis(2, 60,50 , 4, 1174);
          
          delay(2000);
          //허스키 렌즈 가 인식 될때까지 계속 이동
          while (huskylens_check != 1) {
            dxl.setGoalVelocity(4, 1224);
            dxl.setGoalVelocity(18, 200);
            if (!huskylens.request()) {
              DEBUG_SERIAL.println(F("허스키 렌즈 연결 불가"));
            } else if (!huskylens.isLearned()) {
              DEBUG_SERIAL.println(F("학습데이터 없음"));
            } else if (!huskylens.available()) {
              DEBUG_SERIAL.println(F("태그 없음"));
            } else {
              while (huskylens.available()) {
                //Serial모니터에 계속 출력
                HUSKYLENSResult result = huskylens.read();
                DEBUG_SERIAL.print("X : ");
                DEBUG_SERIAL.print(result.xCenter);
                DEBUG_SERIAL.print(", Y : ");
                DEBUG_SERIAL.println(result.yCenter);
                if (result.ID == 5) {
                  DEBUG_SERIAL.println("stop");
                  dxl.setGoalVelocity(4, 0);
                  dxl.setGoalVelocity(18, 0);
                  huskylens_check = 1;
                }
              }
            }
          }
        }
      } else if (8 <= cm && cm <= 11) {
        /*
      장치가 곡선을 처음 본 시점에서 앞 4번 다이나믹 쉘에 속도를 높혀 각도를 보다 편하게 작동시킨다.
      */
        dxl.setGoalVelocity(4, motor_speed + 200);
        dxl.setGoalVelocity(18, motor_speed + 1024);
        DEBUG_SERIAL.println("back slow");
      } else if (22 <= cm && cm <= 34) {
        /*
      장치의 초음파 센서가 곡선을 빠저나온 시점을 타겟했다. 이는 해당 범위에 해당하면 18번 모터를 앞 4번 모터에 상대적으로 빠르게 작동시켜 움직임을 유연하게 만든다.
      */
        dxl.setGoalVelocity(4, motor_speed);
        dxl.setGoalVelocity(18, motor_speed + 1224);
        DEBUG_SERIAL.println("slow");
      } else {
        /*
      장치에 해당 사항이 없으면 트렉을 올라가고 있다는 뜻이니 그냥 가도록 만들었다.
      */
        dxl.setGoalVelocity(4, motor_speed - 100);
        dxl.setGoalVelocity(18, motor_speed + 924);
        DEBUG_SERIAL.println("go");
      }
    }
  }
}
