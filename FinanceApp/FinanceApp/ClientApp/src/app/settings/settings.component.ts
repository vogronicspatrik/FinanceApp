import {Component, OnInit, ViewChild} from '@angular/core';
import {ColorEvent} from 'ngx-color';
import {Category} from "../models/Category";
import {CategoryService} from "../services/category.service";
import {NgForm} from "@angular/forms";

@Component({
  selector: 'app-settings',
  templateUrl: './settings.component.html',
  styleUrls: ['./settings.component.css']
})
export class SettingsComponent implements OnInit {
  @ViewChild('categoryForm', {static: false}) form: any;

  category: Category = new Category('', 'Entertainment', '#417505');
  categories: Category[] = [];
  deleteCategoryList: number[] = [];

  constructor(private categoryService: CategoryService) {
  }

  ngOnInit(): void {
    this.categoryService.getCategories().subscribe(categories => {
        this.categories = categories
      }
    );
  }

  onChangeColor($event: ColorEvent): void {
    this.category.color = $event.color.hex;
  }

  changeDeleteStatus(id: string) {
    const idNumber = Number(id);
    if (this.deleteCategoryList.includes(idNumber)) {
      this.deleteCategoryList.splice(this.deleteCategoryList.indexOf(idNumber), 1);
    } else {
      this.deleteCategoryList.push(idNumber);
    }
  }

  onSubmit(categoryForm: NgForm) {
    this.categoryService.addCategory(this.category).subscribe(category => {
        this.categories.push(category);
        categoryForm.resetForm();
        this.category = new Category('', '', '#FFFFFF');
      }
    );
  }

  deleteCategories() {
    this.categoryService.deleteCategories(this.deleteCategoryList).subscribe(deletedIds => {
      for (let i = 0; i < this.categories.length; i++) {
        if (deletedIds.includes(Number(this.categories[i].id))) {
          this.categories.splice(i, 1);
          i--;
        }
      }
      this.deleteCategoryList = [];
    });
  }
}
