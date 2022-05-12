import {Component, EventEmitter, Input, OnInit, Output} from '@angular/core';
import {Bond} from "../../models/Bond";
import {BondService} from "../../services/bond.service";

@Component({
  selector: 'app-bond',
  templateUrl: './bond.component.html',
  styleUrls: ['./bond.component.css']
})
export class BondComponent implements OnInit {
  @Input() bond!: Bond;
  @Output() deleteBond = new EventEmitter<any>();
  open: boolean = false;

  constructor(private bondService: BondService) {
  }

  ngOnInit(): void {
  }

  delete() {
    this.bondService.deleteBond(this.bond.id).subscribe(any => {
      this.deleteBond.emit();
    });
  }

}
