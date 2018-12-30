import { Component, OnInit } from '@angular/core';
import { AuthService } from './_services/auth.service';

@Component({
  selector: 'mate-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.scss']
})
export class AppComponent implements OnInit {
  title = 'Mate-Match';

  constructor(private authService: AuthService) {}

  ngOnInit(): void {
    this.authService.checkToken();
  }

}
