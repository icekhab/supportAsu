import 'package:angular/angular.dart';
import 'package:angular_forms/angular_forms.dart';

@Component(
    selector: 'claim-list',
    templateUrl: 'claim-list.component.html',
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
class ClaimListComponent {
  ClaimListComponent() {
  }
}
