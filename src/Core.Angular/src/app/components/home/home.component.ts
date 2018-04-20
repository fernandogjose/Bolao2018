import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { SharedService } from '../../services/shared.service';
import { UserPointClassification } from '../../models/user-point-classification.model';
import { HomeService } from '../../services/home.service';

@Component({
  selector: 'app-home',
  templateUrl: './home.component.html',
  styleUrls: ['./home.component.css']
})
export class HomeComponent implements OnInit {

  shared: SharedService;
  userPointClassifications: UserPointClassification[];

  constructor(private homeService: HomeService, private router: Router) {
    this.shared = SharedService.getInstance();
  }

  ngOnInit() {
    this.list();
  }

  list() {
    this.homeService
      .list()
      .subscribe((userPointClassifications: UserPointClassification[]) => {
        this.userPointClassifications = userPointClassifications;
      }, error => {
        if (error.status == 401) {
          this.shared.showTemplate.emit(false);
          this.shared.user = null;
          this.router.navigate(['/login']);
        }
      });
  }
}