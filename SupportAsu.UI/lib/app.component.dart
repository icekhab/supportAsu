import 'package:angular/angular.dart';
import 'package:angular_forms/angular_forms.dart';
import 'package:angular_router/angular_router.dart';
import 'shared/services/local-storage.service.dart';
import 'shared/services/user.service.dart';
import 'src/account/login.component.dart';
import 'src/menu/menu.component.dart';

@Component(
    selector: 'my-app',
    template: '<router-outlet></router-outlet>',
    directives: const [
      CORE_DIRECTIVES,
      ROUTER_DIRECTIVES,
      formDirectives,
      NgIf,
      NgFormControl,
      NgForm,
      NgControl,
      NgModel
    ],
    providers: const [
      LocalStorageService,
      UserService,
      FormBuilder
    ])
@RouteConfig(const [
  const Route(path: '/main/...', name: 'Menu', component: MenuComponent, useAsDefault: true),
  const Route(path: '/login', name: 'Login', component: LoginComponent)
])
class AppComponent {}
