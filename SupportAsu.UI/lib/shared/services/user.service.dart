import '../models/user.dart';
import 'dart:async';
import 'dart:convert';

import 'local-storage.service.dart';
import 'package:angular/angular.dart';
import 'package:http/http.dart';

@Injectable()
class UserService {
  static final _headers = {'Content-Type': 'application/json'};
  static const _url = 'http://localhost:53097'; // URL to web API

  final Client _http;
  final LocalStorageService _localStorageService;

  UserService(this._http, this._localStorageService);

  Future login(String userName, String password) async {
    try {
      var response = await _http.post('${_url}/token',
          body: 'userName=${userName}&password=${password}&grant_type=password',
          headers: _headers);
      _localStorageService.setUser(JSON.encode(_extractData(response)));
    } catch (e) {
      throw _handleError(e);
    }
  }

  dynamic _extractData(Response resp) => JSON.decode(resp.body);

  Exception _handleError(dynamic e) {
    print(e); // for demo purposes only
    return new Exception('Server error; cause: $e');
  }
}
