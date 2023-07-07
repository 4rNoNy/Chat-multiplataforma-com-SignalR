import 'package:agu_chat/temp_data/user_modal.dart';
import 'package:agu_chat/views/profile_screen.dart';

import '../temp_data/post_model.dart';
import '../views/chat_screen.dart';
import '../views/feed_screen.dart';

const double paddingAll = 10;
List pages = [ChatScreen(), FeedScreen(), ProfileScreen()];


const serverName =
    "[SEU IP]"; 
const urlBase = "http://$serverName:5291";
const hubUrl = "http://$serverName:5291/hubs/";

const fontSize = 22.0;

const appId = "[APP ID ONE SIGNAL]"; 

final List<String> iconsCustom = [
  '😊',
  '😆',
  '😅',
  '🤣',
  '😂',
  '😍',
  '😘',
  '❤️',
  '💘',
  '🐶',
  '🐵',
  '🦊',
  '🐴',
  '🐷',
  '🐔'
];

final users = [
  UserTemp(
    profileImageUrl: 'assets/images/user0.jpg',
    backgroundImageUrl: 'assets/images/user_background.jpg',
    name: 'nome',
    following: 453,
    followers: 937,
    posts: [],
    favorites: [],
  ),
  UserTemp(
    profileImageUrl: 'assets/images/user1.jpg',
    backgroundImageUrl: 'assets/images/user_background.jpg',
    name: 'nome',
    following: 453,
    followers: 937,
    posts: [],
    favorites: [],
  ),
  UserTemp(
    profileImageUrl: 'assets/images/user2.jpg',
    backgroundImageUrl: 'assets/images/user_background.jpg',
    name: 'nome',
    following: 453,
    followers: 937,
    posts: [],
    favorites: [],
  ),
  UserTemp(
    profileImageUrl: 'assets/images/user3.jpg',
    backgroundImageUrl: 'assets/images/user_background.jpg',
    name: 'nome',
    following: 453,
    followers: 937,
    posts: [],
    favorites: [],
  ),
  UserTemp(
    profileImageUrl: 'assets/images/user4.jpg',
    backgroundImageUrl: 'assets/images/user_background.jpg',
    name: 'nome',
    following: 453,
    followers: 937,
    posts: [],
    favorites: [],
  ),
  UserTemp(
    profileImageUrl: 'assets/images/user5.jpg',
    backgroundImageUrl: 'assets/images/user_background.jpg',
    name: 'nome',
    following: 453,
    followers: 937,
    posts: [],
    favorites: [],
  ),
  UserTemp(
    profileImageUrl: 'assets/images/user7.jpg',
    backgroundImageUrl: 'assets/images/user_background.jpg',
    name: 'nome',
    following: 453,
    followers: 937,
    posts: [],
    favorites: [],
  )
];

