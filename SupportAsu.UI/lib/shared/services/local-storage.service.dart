import '../models/user.dart';
import 'dart:convert';
import 'dart:html';
import 'package:angular/angular.dart';

@Injectable()
class LocalStorageService {
  User user;
  Storage localStorage;

  LocalStorageService() {
    Storage localStorage = window.localStorage;
    if (localStorage.containsKey('user')) {
      user = new User.fromLocalStorage(JSON.decode(localStorage['users']));
    }
  }

  void setUser(User user) {
    localStorage['user'] = JSON.encode(user);
    user = user;
  }

  User getUser() {
    return user;
  }
}
