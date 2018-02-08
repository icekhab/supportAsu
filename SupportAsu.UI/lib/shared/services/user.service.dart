import '../models/result.dart';
import '../models/user.dart';
import 'dart:async';
import 'dart:convert';

import 'local-storage.service.dart';
import 'package:angular/angular.dart';
import 'package:angular_router/angular_router.dart';
import 'package:http/http.dart';

@Injectable()
class UserService {
  static final _headers = {'Content-Type': 'application/json'};
  static const _url = 'http://localhost:53097'; // URL to web API

  final Client _http;
  final LocalStorageService _localStorageService;
  final Router _router;

  UserService(this._http, this._localStorageService, this._router);

  Future<Result<User>> signIn(String userName, String password) async {
    try {
      var response = await _http.post('${_url}/token',
          body: 'userName=${userName}&password=${password}&grant_type=password',
          headers: _headers);
      var result = new Result<User>();
      if (response.statusCode == 400) {
        result.isError = true;
        result.error = _extractData(response)['error_description'];
      } else if (response.statusCode == 500) {
        result.data =
            _localStorageService.setUser(JSON.encode(_extractData(response)));
      }
      return result;
    } catch (e) {
      throw _handleError(e);
    }
  }

  void logOff() {
    _localStorageService.removeUser();
    _router.navigateByUrl('/login');
  }

  dynamic _extractData(Response resp) => JSON.decode(resp.body);

  Exception _handleError(dynamic e) {
    print(e); // for demo purposes only
    return new Exception('Server error; cause: $e');
  }
}
