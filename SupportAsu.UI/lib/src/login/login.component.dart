import '../../shared/models/login.dart';
import '../../shared/services/user.service.dart';
import 'dart:async';
import 'package:angular/angular.dart';
import 'package:angular_forms/angular_forms.dart';

@Component(
    selector: 'login',
    templateUrl: 'login.component.html',
    directives: const [
      CORE_DIRECTIVES,
      formDirectives,
      NgIf,
      NgFormControl,
      NgForm,
      NgControl,
      NgModel
    ],
    exportAs: 'ngForm')
class LoginComponent {
  final UserService _userService;
  ControlGroup loginForm;
  Login model = new Login();

  LoginComponent(UserService this._userService, FormBuilder fb) {
    print(_userService);
    print(fb);
    initForm(fb);
  }

  void initForm(FormBuilder fb) {
    loginForm = fb.group({
      'userName': ['', Validators.required],
      'password': ['', Validators.required],
    });
  }

  Future signIn(NgForm form) async {
    if (form.valid) {
      var userName = form.value['userName'];
      var password = form.value['password'];
      await _userService.login(form.value['userName'], form.value['password']);
    }
  }
}
