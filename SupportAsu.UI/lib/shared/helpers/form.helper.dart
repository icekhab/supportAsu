import 'package:angular_forms/angular_forms.dart';

class FormHelper {
  static void setControlPristine(ControlGroup form) {
    form.controls.forEach((key, value) => value.markAsTouched());
  }
}
