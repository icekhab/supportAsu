import '../../shared/models/filter.dart';
import 'package:angular/angular.dart';
import 'package:angular_forms/angular_forms.dart';

@Component(
    selector: 'claim-filter',
    templateUrl: 'filter.component.html',
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
class ClaimFilterComponent {
  Filter filter;
  int total = 100;
  bool showFilter = false;

  ClaimFilterComponent() {
    filter = new Filter.init(10, 0);
  }
}

const List heroSwitchComponents = const [
  ClaimFilterComponent
];