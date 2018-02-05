import '../../shared/models/user.dart';
import '../../shared/services/local-storage.service.dart';
import '../claim/claim-list.component.dart';
import '../task/task-list.component.dart';
import 'dart:async';
import 'package:angular/angular.dart';
import 'package:angular_forms/angular_forms.dart';
import 'package:angular_router/angular_router.dart';

@Component(
    selector: 'menu',
    templateUrl: 'menu.component.html',
    directives: const [
      CORE_DIRECTIVES,
      formDirectives,
      NgIf,
      NgSwitch,
      NgFormControl,
      NgForm,
      NgControl,
      NgModel,
      ROUTER_DIRECTIVES
    ],
    exportAs: 'ngForm')
@RouteConfig(const [
  const Route(
      path: '/tasks/...', name: 'TaskList', component: TaskListComponent),
  const Route(
      path: '/claims/...', name: 'ClaimList', component: ClaimListComponent)
])
class MenuComponent implements OnActivate {
  final LocalStorageService _localStorageService;
  final Router _router;
  User user;

  MenuComponent(this._localStorageService, this._router) {
    user = _localStorageService.getUser();
  }

  void logOff() {}
  // TODO: implement fn
  @override
  Future<bool> routerOnActivate(
      ComponentInstruction next, ComponentInstruction prev) {
    var user = _localStorageService.getUser();

    if (user == null) {
      _router.navigateByUrl('/login');
      return false as FutureOr<bool>;
    } else {
      String homePage;
      switch (user.role) {
        case 'Support Admins':
          {
            homePage = 'taks';
            break;
          }
        case 'Support Interns':
          {
            homePage = 'claims';
            break;
          }
      }
      _router.navigateByUrl('/${homePage}');
      return false as FutureOr<bool>;
    }
  }
}
