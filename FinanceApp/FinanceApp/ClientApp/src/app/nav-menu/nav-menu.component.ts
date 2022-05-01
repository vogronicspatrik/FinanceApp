import {Component, EventEmitter, Input, Output} from '@angular/core';

@Component({
  selector: 'app-nav-menu',
  templateUrl: './nav-menu.component.html',
  styleUrls: ['./nav-menu.component.css']
})
export class NavMenuComponent {
  @Input() currentPage: string = '';
  @Output() switchLanguage = new EventEmitter<string>();
  @Output() updatePage = new EventEmitter<string>();
}
