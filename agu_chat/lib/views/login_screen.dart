import 'dart:convert';

import 'package:agu_chat/models/auth.dart';
import 'package:flutter/material.dart';
import 'package:fluttertoast/fluttertoast.dart';
import 'package:shared_preferences/shared_preferences.dart';

import '../models/user_login.dart';
import '../models/user_response.dart';
import '../temp_data/widgets/curve_clipper.dart';
import '../utils/global.dart';

enum FormType { login, register }

class LoginScreen extends StatefulWidget {
  final VoidCallback onSignedIn;
  final String title;

  LoginScreen({Key? key, required this.title, required this.onSignedIn})
      : super(key: key);

  @override
  State<LoginScreen> createState() {
    return LoginScreenState();
  }
}

class LoginScreenState extends State<LoginScreen> {
  static final formKey = GlobalKey<FormState>();
  FormType _formType = FormType.login;
  String _authHint = '';
  final UserLogin _user = UserLogin(userName: '', password: '');
  AuthASP auth = AuthASP();
  bool loading = false;
  final Future<SharedPreferences> _prefs = SharedPreferences.getInstance();

  @override
  void initState() {
    super.initState();
  }

  bool validateAndSave() {
    final form = formKey.currentState;
    if (form!.validate()) {
      form.save();
      return true;
    }
    return false;
  }

  void validateAndSubmit() async {
    if (validateAndSave()) {
      if (_formType == FormType.login) {
        setState(() {
          loading = true;
        });

        UserResponse resp = await auth.signIn(_user.userName, _user.password);
        if (resp.error == '200') {
          Global.user = resp.user;
          widget.onSignedIn();
          //save local storages
          final SharedPreferences prefs = await _prefs;
          final json = jsonEncode(resp.user!.toJson());
          final _counter = prefs.setString('user', json).then((bool success) {
            return 0;
          });
          Fluttertoast.showToast(
              msg: "Login com sucesso!",
              toastLength: Toast.LENGTH_SHORT,
              gravity: ToastGravity.CENTER,
              timeInSecForIosWeb: 1,
              backgroundColor: Colors.green,
              textColor: Colors.white,
              fontSize: 16.0
          );
        } else {
          setState(() {
            _authHint = resp.error!;
            loading = false;
          });
        }
      } else {
        /*UserResponse resp = await widget.auth.register(_user);
        if (resp.error == '200') {
          moveToLogin();
        } else {
          setState(() {
            _authHint = resp.error;
          });
        }*/
      }
    }
  }

  void moveToRegister() {
    formKey.currentState!.reset();
    setState(() {
      _formType = FormType.register;
      _authHint = '';
    });
  }

  void moveToLogin() {
    formKey.currentState!.reset();
    setState(() {
      _formType = FormType.login;
      _authHint = '';
    });
  }

  List<Widget> usernameAndPassword() {
  return [
    Padding(
      padding: const EdgeInsets.only(bottom: 30),
      child: TextFormField(
        key: const Key('Login'),
        keyboardType: TextInputType.text,
        decoration: InputDecoration(
          labelText: 'Login',
          labelStyle: TextStyle(color: Colors.black54), // Cor do rótulo
          prefixIcon: Icon(Icons.email, color: Colors.grey), // Cor do ícone
          filled: true,
          fillColor: Colors.grey[200], // Cor do fundo do campo
          border: OutlineInputBorder(
            borderRadius: BorderRadius.circular(10)
          ),
        ),
        autocorrect: false,
        validator: (val) =>
            val!.isEmpty ? 'O nome de usuário não pode estar vazio.' : null,
        onSaved: (val) => _user.userName = val!,
      ),
    ),
    Padding(
      padding: const EdgeInsets.only(bottom: 10),
      child: TextFormField(
        key: const Key('Senha'),
        decoration: InputDecoration(
          labelText: 'Senha',
          labelStyle: TextStyle(color: Colors.black54), // Cor do rótulo
          prefixIcon: Icon(Icons.lock, color: Colors.grey), // Cor do ícone
          filled: true,
          fillColor: Colors.grey[200], // Cor do fundo do campo
          border: OutlineInputBorder(
            borderRadius: BorderRadius.circular(10)
          ),
        ),
        obscureText: true,
        autocorrect: false,
        validator: (val) =>
            val!.isEmpty ? 'A senha não pode estar em branco.' : null,
        onSaved: (val) => _user.password = val!,
      ),
    ),
  ];
}


List<Widget> submitWidgets() {
  switch (_formType) {
    case FormType.login:
      return [
        Padding(
          padding: const EdgeInsets.only(bottom: 100, top: 40, left: 100,right: 100),
          child: SizedBox(
            width: 50,
            height: 70,
            child: ElevatedButton(
              onPressed: loading ? null : validateAndSubmit,
              child: loading
                  ? const Center(
                      child: CircularProgressIndicator(color: Colors.white),
                    )
                  : const Text(
                      'Login',
                      style: TextStyle(color: Colors.white),
                    ),
              style: ElevatedButton.styleFrom(
                primary: Colors.blue[300],
                shape: RoundedRectangleBorder(
                  borderRadius: BorderRadius.circular(50),
                ),
              ),
            ),
          ),
        ),
        TextButton(
          onPressed: moveToRegister,
          child: const Text(
            "Precisa de uma conta? Registre-se",
            style: TextStyle(color: Colors.white),
          ),
        ),
      ];
    case FormType.register:
      return [
        Padding(
          padding: const EdgeInsets.only(bottom: 10),
          child: SizedBox(
            width: 200,
            height: 50,
            child: ElevatedButton(
              onPressed: validateAndSubmit,
              child: const Text(
                'register',
                style: TextStyle(color: Colors.white),
              ),
              style: ElevatedButton.styleFrom(
                primary: Colors.green,
                shape: RoundedRectangleBorder(
                  borderRadius: BorderRadius.circular(10),
                ),
              ),
            ),
          ),
        ),
        TextButton(
          onPressed: moveToLogin,
          child: const Text(
            "Have an account? Login",
            style: TextStyle(color: Colors.white),
          ),
        ),
      ];
  }
}



  @override
  Widget build(BuildContext context) {
    return Scaffold(
      body: Stack(
        children: [
          Container(
            color: Color.fromARGB(225, 33, 33, 33),
            height: double.infinity,
            width: double.infinity,
          ),
          SingleChildScrollView(
            child: SafeArea(
              child: Column(
                children: [
                  //ClipPath is test UI
                  ClipPath(
                    clipper: CurveClipper(),
                    child: Image(
                      height: MediaQuery.of(context).size.height / 2.5,
                      width: double.infinity,
                      image: AssetImage('assets/images/login_background.jpg'),
                      fit: BoxFit.cover,
                    ),
                  ),
                  SizedBox(height: 20),
                  Padding(
                    padding: const EdgeInsets.symmetric(horizontal: 20),
                    child: Form(
                      key: formKey,
                      child: Column(
                        crossAxisAlignment: CrossAxisAlignment.stretch,
                        children: usernameAndPassword() + submitWidgets(),
                      ),
                    ),
                  ),
                  Text(
                    _authHint,
                    style: TextStyle(
                      fontSize: 20,
                      color: Colors.grey.withOpacity(0.6),
                    ),
                  )
                ],
              ),
            ),
          ),
        ],
      ),
    );
  }
}
