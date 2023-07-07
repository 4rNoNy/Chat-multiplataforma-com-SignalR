import 'package:agu_chat/models/member.dart';
import 'package:flutter/cupertino.dart';
import 'package:flutter/material.dart';
import 'package:onesignal_flutter/onesignal_flutter.dart';
import '../controllers/presenceHub.dart';
import '../services/oneSignalService.dart';
import '../utils/const.dart';
import 'package:get/get.dart';
import '../utils/global.dart';

class BottomNavbar extends StatefulWidget{
  const BottomNavbar({super.key});

  @override
  State<BottomNavbar> createState() => BottomNavbarState();
}

class BottomNavbarState extends State<BottomNavbar>{
  int pageIndex = 0;
  final presenceHubController = Get.put(PresenceHubController());
  OneSignalService oneSignalService = OneSignalService();

  @override
  void initState() {
    super.initState();
    presenceHubController.createHubConnection(Global.user);
    setState(() {
      pageIndex = 0;
    });
    initPlatformState();
  }

  Future<void> initPlatformState() async {
    OneSignal.shared.setAppId("10028d9b-1c25-4395-901a-aa30190eb7d4");
    final status = await OneSignal.shared.getDeviceState();
    final String? osUserID = status?.userId;
    Global.playerId = osUserID;

    if(osUserID != null){
      oneSignalService.postPlayerId(osUserID).then((value) => print(value));
    }

    OneSignal.shared.setNotificationWillShowInForegroundHandler((OSNotificationReceivedEvent event) {
      // Será chamado sempre que uma notificação for recebida em primeiro plano
      // Exibir notificação, passar parâmetro nulo para não exibir a notificação
      event.complete(event.notification);
    });

    OneSignal.shared.setNotificationOpenedHandler((OSNotificationOpenedResult result) {
      // Será chamado sempre que uma notificação for aberta/botão pressionado.
    });

    OneSignal.shared.setPermissionObserver((OSPermissionStateChanges changes) {
    });

    OneSignal.shared.setSubscriptionObserver((OSSubscriptionStateChanges changes) {
    });

    OneSignal.shared.setEmailSubscriptionObserver((OSEmailSubscriptionStateChanges emailChanges) {
    });
  }

  @override
  Widget build (BuildContext context){
    return Scaffold(
      body: pages[pageIndex],
      bottomNavigationBar: CupertinoTabBar(
        onTap: (index){
          setState(() {
            pageIndex = index;
          });
        },
        currentIndex: pageIndex,
        items: const [
          BottomNavigationBarItem(icon: Icon(Icons.chat, size: 35,), label: 'Chat'),
          BottomNavigationBarItem(icon: Icon(Icons.rss_feed, size: 35,), label: 'Feed'),
          BottomNavigationBarItem(icon: Icon(Icons.account_box, size: 35,), label: 'Profile'),
        ],),
    );
  }
}