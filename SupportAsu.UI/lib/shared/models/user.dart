import 'package:dartson/dartson.dart';

@Entity()
class User {
  @Property(name:"access_token")
  String accessToken;
  @Property(name:"token_type")
  String tokenType;
  @Property(name:"expires_in")
  int expiresIn;
  @Property(name:"refresh_token")
  String refreshToken;
  @Property(name:"login")
  String login;
  @Property(name:"name")
  String name;
  @Property(name:"role")
  String role;
  @Property(name:".issued")
  DateTime issued;
  @Property(name:".expires")
  DateTime expires;
}