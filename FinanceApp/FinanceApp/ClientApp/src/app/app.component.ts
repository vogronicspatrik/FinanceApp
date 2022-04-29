import {Component} from '@angular/core';
import {TranslateService} from "@ngx-translate/core";

import * as $ from "jquery";

@Component({
  selector: 'app-root',
  templateUrl: './app.component.html'
})
export class AppComponent {
  title = 'app';
  currentPage: string = '';

  constructor(
    public translate: TranslateService
  ) {
    translate.addLangs(['en', 'hun']);
    translate.setDefaultLang('en');
  }

  switchLanguage(lang: string) {
    this.translate.use(lang);
  }

  ngOnInit() {
    $(document).ready(function () {
      $('#sidebarCollapse').on('click', function () {
        $('#sidebar').toggleClass('active');
      });
    });
  }

  receiveMessage($event: any) {
    this.currentPage = $event;
  }
}
