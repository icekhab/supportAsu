import '../../shared/models/filter.dart';
import 'filter.component.dart';
import 'package:angular/angular.dart';
import 'package:angular_forms/angular_forms.dart';

@Component(
    selector: 'claim-list',
    templateUrl: 'list.component.html',
    directives: const [
      CORE_DIRECTIVES,
      formDirectives,
      NgIf,
      NgFormControl,
      NgForm,
      NgControl,
      NgModel,
      ClaimFilterComponent
    ],
    exportAs: 'ngForm')
class ClaimListComponent {
  Filter filter;
  int total = 100;
  ClaimListComponent() {
    filter = new Filter.init(10, 0);
  }
}