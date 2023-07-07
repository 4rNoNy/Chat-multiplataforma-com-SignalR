import 'dart:convert';
import 'package:agu_chat/views/login_screen.dart';
import 'package:flutter/material.dart';
import 'package:flutter/services.dart';
import 'package:flutter_native_splash/flutter_native_splash.dart';
import '../models/user.dart';
import '../utils/global.dart';
import 'bottom_navbar.dart';
import 'package:shared_preferences/shared_preferences.dart';

enum AuthStatus {
  notSignedIn,
  signedIn,
}

class RootScreen extends StatefulWidget {
  @override
  State<RootScreen> createState() {
    return RootScreenState();
  }
}

class RootScreenState extends State<RootScreen> with WidgetsBindingObserver {
  AuthStatus authStatus = AuthStatus.notSignedIn;
  final Future<SharedPreferences> _prefs = SharedPreferences.getInstance();
  //AppLifecycleState? _notification;
  static const platform = MethodChannel('agu.chat/signalR');

  void _updateAuthStatus(AuthStatus status) {
    setState(() {
      authStatus = status;
    });
  }

  Future<void> startService(String token, String currentUsername) async {
    try {
      
      await platform.invokeMethod('startSignalrService',
          {"token": token, "currentUsername": currentUsername});
    } on PlatformException catch (e) {
      print(e.toString());
    }
  }
  @override
  void dispose() {
    super.dispose();
    WidgetsBinding.instance.removeObserver(this);
  }

  @override
  void initState() {
    super.initState();
    WidgetsBinding.instance.addObserver(this);
    Global.myStream!.counterStream.listen((event) {
      if (event) {
        //print(event);
        _updateAuthStatus(AuthStatus.notSignedIn);
      }
    });
    //ler de armazenamentos locais
    final user = _prefs.then((SharedPreferences prefs) {
      var json = prefs.getString('user');
      if (json == null) {
        _updateAuthStatus(AuthStatus.notSignedIn);
        return null;
      }
      Map<String, dynamic> userJson = jsonDecode(json);
      final tempUser = User.fromJson(userJson);
      Global.user = tempUser;
      //chamada para android nativo
      startService(Global.user!.token, Global.user!.username);

      _updateAuthStatus(AuthStatus.signedIn);
      return tempUser;
    });
    FlutterNativeSplash.remove();
  }

  @override
  Widget build(BuildContext context) {
    switch (authStatus) {
      case AuthStatus.notSignedIn:
        return LoginScreen(
          title: 'Login',
          onSignedIn: () => {
            _updateAuthStatus(AuthStatus.signedIn),
            startService(Global.user!.token, Global.user!.username)
          },
        );
      case AuthStatus.signedIn:
        return BottomNavbar();
    }
  }
}
