import '../../shared/helpers/form.helper.dart';
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
  final FormBuilder _fb;
  ControlGroup loginForm;
  Login model = new Login();
  String error;

  LoginComponent(this._userService, this._fb, this._router) {
    print(_userService);
    initForm();
  }

  void initForm() {
    loginForm = _fb.group({
      'userName': ['', Validators.required],
      'password': ['', Validators.required],
    });
  }

  Future signIn(NgForm form) async {
    FormHelper.setControlPristine(loginForm);
    if (form.valid) {
      var result = await _userService.signIn(
          form.value['userName'], form.value['password']);
      if (result.isError) {
        error = result.error;
        return;
      }
      _router.navigate(['Menu']);
    }
  }
}
