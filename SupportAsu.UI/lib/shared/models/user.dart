class User {
  String accessToken;
  String tokenType;
  int expiresIn;
  String refreshToken;
  String login;
  String name;
  String role;
  DateTime issued;
  DateTime expires;

  User.fromApi(Map json) {
    this.accessToken = json['access_token'];
    this.tokenType = json['token_type'];
    this.expiresIn = json['expires_in'];
    this.refreshToken = json['refresh_token'];
    this.login = json['login'];
    this.name = json['name'];
    this.role = json['role'];
    this.issued = json['.issued'];
    this.expires = json['.expires'];
  }

  User.fromLocalStorage(Map json) {
    this.accessToken = json['accessToken'];
    this.tokenType = json['tokenType'];
    this.expiresIn = json['expiresIn'];
    this.refreshToken = json['refreshToken'];
    this.login = json['login'];
    this.name = json['name'];
    this.role = json['role'];
    this.issued = json['issued'];
    this.expires = json['expires'];
  }
}
