import {Component, EventEmitter, Input, OnInit, Output} from '@angular/core';
import {ValuableItem} from "../../models/ValuableItem";
import {ValuableItemService} from "../../services/valuable-item.service";

@Component({
  selector: 'app-valuable-item',
  templateUrl: './valuable-item.component.html',
  styleUrls: ['./valuable-item.component.css']
})
export class ValuableItemComponent implements OnInit {
  @Input() valuableItem!: ValuableItem;
  @Output() updateItems = new EventEmitter<any>();
  open: boolean = false;

  constructor(private valuableItemService: ValuableItemService) {
  }

  ngOnInit(): void {
  }

  deleteItem(id: string) {
    this.valuableItemService.deleteValuableItem(id).subscribe(any => {
      this.updateItems.emit();
    });
  }

}
