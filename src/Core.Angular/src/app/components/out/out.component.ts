import { Component, OnInit } from '@angular/core';
import { SharedService } from '../../services/shared.service';
import { HomeService } from '../../services/home.service';
import { UserPointClassification } from '../../models/user-point-classification.model';

@Component({
  selector: 'app-out',
  templateUrl: './out.component.html',
  styleUrls: ['./out.component.css']
})
export class OutComponent implements OnInit {

  shared: SharedService;

  constructor(private homeService: HomeService) {
    this.shared = SharedService.getInstance();
  }

  ngOnInit() {
    this.classificationList();
  }

  classificationList() {
    this.homeService
      .list()
      .subscribe((userPointClassifications: UserPointClassification[]) => {
        this.shared.showTemplate.emit(true)
      });
  }

}
