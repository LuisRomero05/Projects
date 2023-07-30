import 'package:flutter/material.dart';

void main() {
  runApp(MyApp());
}

class MyApp extends StatelessWidget {
  @override
  Widget build(BuildContext context) {
    return const MaterialApp(
      debugShowCheckedModeBanner: false,
      home: Scaffold(
        backgroundColor: Color.fromARGB(255, 255, 255, 255),
        body: SingleChildScrollView( // Wrap the content with SingleChildScrollView
          child: Center(
            child: Column(
              mainAxisAlignment: MainAxisAlignment.center,
              crossAxisAlignment: CrossAxisAlignment.center,
              children: [
                SizedBox(
                  height: 30.0,
                  width: 10.0,
                ),
                Align(
                  alignment: Alignment.topLeft, // Aligns to the top left
                  child: Icon(
                    Icons.arrow_back,
                    color: Colors.black,
                  ),
                ),
                SizedBox(
                  height: 10.0,
                  width: 200.0,
                ),
                CircleAvatar(
                  radius: 50.0,
                  backgroundImage: AssetImage("assets/images/p.png"),
                ),
                SizedBox(
                  height: 15.0,
                  width: 15.0,
                ),
               Text(
                    "Pascualillo",
                    style: TextStyle(
                        fontSize: 25.0,
                        color: Color.fromARGB(255, 0, 0, 0),
                        fontFamily: "Pacifico"),
                  ),
                  SizedBox(
                    height: 5.0,
                    width: 350,
                  ),
                  //mergen #1
                  Card(
                    color: Colors.white,
                    margin:
                        EdgeInsets.symmetric(horizontal: 30.0, vertical: 30.0),
                    child: Row(
                      mainAxisAlignment: MainAxisAlignment.spaceEvenly,
                      children: [
                        //icono 1
                        Column(
                          children: [
                            Icon(
                              Icons.phone,
                              color: Colors.black,
                            ),
                            SizedBox(
                              height: 15.0,
                            ),
                            Text(
                              "Llamar",
                              style: TextStyle(
                                  color: Colors.black,
                                  fontWeight: FontWeight.bold,
                                  fontSize: 12.0,
                                  fontFamily: "SourceSansPro-Regular"),
                            ),
                          ],
                        ),
                        //icono2
                        Column(
                          children: [
                            Icon(
                              Icons.message_outlined,
                              color: Colors.black,
                            ),
                            SizedBox(
                              height: 10.0,
                            ),
                            Text(
                              "Mensaje",
                              style: TextStyle(
                                  color: Colors.black,
                                  fontWeight: FontWeight.bold,
                                  fontSize: 12.0,
                                  fontFamily: "SourceSansPro-Regular"),
                            ),
                          ],
                        ),
                        //icono3
                        Column(
                          children: [
                            Icon(
                              Icons.video_call_outlined,
                              color: Colors.black,
                            ),
                            SizedBox(
                              height: 10.0,
                            ),
                            Text(
                              "Video",
                              style: TextStyle(
                                  color: Colors.black,
                                  fontWeight: FontWeight.bold,
                                  fontSize: 12.0,
                                  fontFamily: "SourceSansPro-Regular"),
                            ),
                          ],
                        )
                      ],
                    ),
                  ),
                  //fin de parte de arriba
                  Card(
                    color: Colors.white,
                    margin:
                        EdgeInsets.symmetric(horizontal: 30.0, vertical: 5.0),
                    child: Padding(
                      padding: EdgeInsets.symmetric(
                          horizontal: 15.0, vertical: 10.0),
                      child: Column(
                        crossAxisAlignment: CrossAxisAlignment.start,
                        children: [
                          Text(
                            "Informacion de contacto",
                            style: TextStyle(
                              color: Color.fromARGB(255, 0, 0, 0),
                              fontWeight: FontWeight.bold,
                              fontSize: 15.0,
                              fontFamily: "SourceSansPro-Regular",
                            ),
                          ),
                          SizedBox(height: 15.0),
                          Row(
                            children: [
                              Icon(
                                Icons.phone,
                                color: Colors.black,
                                size: 25.0,
                              ),
                              SizedBox(width: 5.0),
                              Column(
                                crossAxisAlignment: CrossAxisAlignment.start,
                                children: [
                                  Text(
                                    "+504 9874-6820",
                                    style: TextStyle(
                                      color: Colors.black,
                                      fontWeight: FontWeight.bold,
                                      fontSize: 12.0,
                                      fontFamily: "SourceSansPro-Regular",
                                    ),
                                  ),
                                  Text(
                                    "Celular",
                                    style: TextStyle(
                                      color: Colors.black,
                                      fontWeight: FontWeight.bold,
                                      fontSize: 11.0,
                                      fontFamily: "SourceSansPro-Regular",
                                    ),
                                  ),
                                ],
                              ),
                              //icono de videollamda
                              SizedBox(
                                width: 85.0,
                              ),
                              Column(
                                children: [
                                  Icon(
                                    Icons.video_call_outlined,
                                    color: Colors.black,
                                    size: 25.0,
                                  ),
                                ],
                              ),
                              //icono de mensaje
                              SizedBox(
                                width: 15.0,
                              ),
                              Column(
                                children: [
                                  Icon(
                                    Icons.message_outlined,
                                    color: Colors.black,
                                    size: 20.0,
                                  ),
                                ],
                              ),
                            ],
                          ),
                          //informacion de whatsApp y telegram
                          SizedBox(
                            height: 15.0,
                          ),
                          //whatsApp #1- mensaje
                          Row(
                            children: [
                              CircleAvatar(
                                radius: 12.0,
                                backgroundImage:
                                    AssetImage("assets/images/whatsApp.png"),
                              ),
                              SizedBox(width: 5.0),
                              Column(
                                crossAxisAlignment: CrossAxisAlignment.start,
                                children: [
                                  Text(
                                    "Enviar Mensaje a +504 9902-9311",
                                    style: TextStyle(
                                      color: Colors.black,
                                      fontWeight: FontWeight.bold,
                                      fontSize: 11.0,
                                      fontFamily: "SourceSansPro-Regular",
                                    ),
                                  ),
                                ],
                              ),
                            ],
                          ),
                          SizedBox(
                            height: 10.0,
                          ),
                          //whatsApp #2- llamada
                          Row(
                            children: [
                              CircleAvatar(
                                radius: 12.0,
                                backgroundImage:
                                    AssetImage("assets/images/whatsApp.png"),
                              ),
                              SizedBox(width: 5.0),
                              Column(
                                crossAxisAlignment: CrossAxisAlignment.start,
                                children: [
                                  Text(
                                    "Llamar a +504 9902-9311",
                                    style: TextStyle(
                                      color: Colors.black,
                                      fontWeight: FontWeight.bold,
                                      fontSize: 11.0,
                                      fontFamily: "SourceSansPro-Regular",
                                    ),
                                  ),
                                ],
                              ),
                            ],
                          ),
                          SizedBox(
                            height: 10.0,
                          ),
                          //whatsApp #3- video-llamada
                          Row(
                            children: [
                              CircleAvatar(
                                radius: 12.0,
                                backgroundImage:
                                    AssetImage("assets/images/whatsApp.png"),
                              ),
                              SizedBox(width: 5.0),
                              Column(
                                crossAxisAlignment: CrossAxisAlignment.start,
                                children: [
                                  Text(
                                    "Videollamar a +504 9902-9311",
                                    style: TextStyle(
                                      color: Colors.black,
                                      fontWeight: FontWeight.bold,
                                      fontSize: 11.0,
                                      fontFamily: "SourceSansPro-Regular",
                                    ),
                                  ),
                                ],
                              ),
                            ],
                          ),
                          //inofrmacion de telegram
                          SizedBox(
                            height: 15.0,
                          ),
                          //telegram #1- mensaje
                          Row(
                            children: [
                              CircleAvatar(
                                radius: 12.0,
                                backgroundImage:
                                    AssetImage("assets/images/telegram.jpg"),
                              ),
                              SizedBox(width: 5.0),
                              Column(
                                crossAxisAlignment: CrossAxisAlignment.start,
                                children: [
                                  Text(
                                    "Mensaje al +504 9902-9311",
                                    style: TextStyle(
                                      color: Colors.black,
                                      fontWeight: FontWeight.bold,
                                      fontSize: 11.0,
                                      fontFamily: "SourceSansPro-Regular",
                                    ),
                                  ),
                                ],
                              ),
                            ],
                          ),
                          SizedBox(
                            height: 10.0,
                          ),
                          //telegram #2- llamada
                          Row(
                            children: [
                              CircleAvatar(
                                radius: 12.0,
                                backgroundImage:
                                    AssetImage("assets/images/telegram.jpg"),
                              ),
                              SizedBox(width: 5.0),
                              Column(
                                crossAxisAlignment: CrossAxisAlignment.start,
                                children: [
                                  Text(
                                    "Llamar de voz al +504 9902-9311",
                                    style: TextStyle(
                                      color: Colors.black,
                                      fontWeight: FontWeight.bold,
                                      fontSize: 11.0,
                                      fontFamily: "SourceSansPro-Regular",
                                    ),
                                  ),
                                ],
                              ),
                            ],
                          ),
                          SizedBox(
                            height: 10.0,
                          ),
                          //telegram #3- video-llamada
                          Row(
                            children: [
                              CircleAvatar(
                                radius: 12.0,
                                backgroundImage:
                                    AssetImage("assets/images/telegram.jpg"),
                              ),
                              SizedBox(width: 5.0),
                              Column(
                                crossAxisAlignment: CrossAxisAlignment.start,
                                children: [
                                  Text(
                                    "Videollamar a +504 9902-9311",
                                    style: TextStyle(
                                      color: Colors.black,
                                      fontWeight: FontWeight.bold,
                                      fontSize: 11.0,
                                      fontFamily: "SourceSansPro-Regular",
                                    ),
                                  ),
                                ],
                              ),
                            ],
                          ),
                        ],
                      ),
                    ),
                  ),
                ],
              ),
            ),
          ),
        ),
      );
    
  }
}