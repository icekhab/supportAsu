class Filter {
  Filter.init(this.take, this.page) {}

  int take;
  int page;
  int get skip => take * (page - 1);
}
