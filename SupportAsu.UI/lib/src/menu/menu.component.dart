import '../../shared/models/user.dart';
import '../../shared/services/local-storage.service.dart';
import '../../shared/static/role.dart';
import '../claim/list.component.dart';
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
      path: '/tasks', name: 'Tasks', component: TaskListComponent),
  const Route(
      path: '/claims', name: 'Claims', component: ClaimListComponent)
])
class MenuComponent implements OnActivate{
  final LocalStorageService _localStorageService;
  final Router _router;
  User user;
  Role role;

  MenuComponent(this._localStorageService, this._router) {
    user = _localStorageService.getUser();    
    role = new Role();
  }

  void logOff() {}

  @override
  Future<bool> routerOnActivate(
      ComponentInstruction next, ComponentInstruction prev) {
    var user = _localStorageService.getUser();
    
    if (user == null) {
      _router.navigateByUrl('/login');
      return false as FutureOr<bool>;
    } else if (_router.root.lastNavigationAttempt == '' || _router.root.lastNavigationAttempt == '/' || _router.root.lastNavigationAttempt == '/main'){
      String homePage;
      switch (user.role) {
        case Role.Administrator:
          {
            homePage = 'Tasks';
            break;
          }
        case Role.Intern:
          {
            homePage = 'Claims';
            break;
          }
      }
      _router.navigate([homePage]);
      
      return true as FutureOr<bool>;
    }
  }
}
