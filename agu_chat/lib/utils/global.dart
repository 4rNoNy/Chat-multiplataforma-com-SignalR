import '../models/user.dart';
import '../stream/my_stream.dart';

class Global {
  static MyStream? myStream;
  static User? user;
  static String? playerId;
  static String? channelName;

  static void clearData() {
    user = null;
    channelName = null;
  }
}
