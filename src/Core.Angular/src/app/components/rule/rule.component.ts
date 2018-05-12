import { Component, OnInit } from '@angular/core';
import { SharedService } from '../../services/shared.service';
import { UserLocalstorage } from '../../localstorage/user.localstorage';
import { UserPointClassification } from '../../models/user-point-classification.model';
import { HomeService } from '../../services/home.service';
import { Router } from '@angular/router';

@Component({
  selector: 'app-rule',
  templateUrl: './rule.component.html',
  styleUrls: ['./rule.component.css']
})
export class RuleComponent implements OnInit {

  shared: SharedService;

  constructor(private homeService: HomeService, private router: Router) {
    this.shared = SharedService.getInstance();
  }

  ngOnInit() {
    this.classificationList();
  }

  classificationList() {
    // this.shared.showTemplate.emit(false);
    this.homeService
      .list()
      .subscribe((userPointClassifications: UserPointClassification[]) => {
        this.shared.showTemplate.emit(true)
      });
  }
}
