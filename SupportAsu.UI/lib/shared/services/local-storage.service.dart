import '../models/user.dart';
import 'dart:html';
import 'package:angular/angular.dart';
import 'package:dartson/dartson.dart';
import 'package:dartson/type_transformer.dart';

@Injectable()
class LocalStorageService {
  User user;
  Storage localStorage;
  Dartson<dynamic> dson;

  LocalStorageService() {
    dson = new Dartson.JSON();
    dson.addTransformer(new DateTimeParser(), DateTime);
    localStorage = window.localStorage;
    if (localStorage.containsKey('user')) {
      user = dson.decode(localStorage['user'], new User());
    }
  }

  User setUser(String newUser) {
    localStorage['user'] = newUser;
    user = dson.decode(localStorage['user'], new User());
    return user;
  }

  void removeUser() {
    localStorage.remove('user');
  }

  User getUser() {
    return user;
  }
}

class DateTimeParser extends TypeTransformer {
  DateTime decode(dynamic value) {
    return value;
  }

  @override
  encode(value) {
    return value.toString();
  }
}
