import '../../shared/models/login.dart';
import '../../shared/services/user.service.dart';
import 'dart:async';
import 'package:angular/angular.dart';
import 'package:angular_forms/angular_forms.dart';
import 'package:angular_router/angular_router.dart';

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
  final Router _router;
  ControlGroup loginForm;
  Login model = new Login();

  LoginComponent(this._userService, FormBuilder fb, this._router) {
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
      //await _userService.login(form.value['userName'], form.value['password']);
      _router.navigateByUrl('/');
    }
  }
}
