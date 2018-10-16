import { Component, OnInit } from '@angular/core';
import { ConfigService } from '../../config/config.service';
import { AppModule } from '../../config/app.module.model';

@Component({
  selector: 'app-sidenav',
  templateUrl: './app-sidenav.component.html',
  styleUrls: ['./app-sidenav.component.css']
})
export class AppSideNavComponent implements OnInit {

  public modules: AppModule[];

  constructor(
    /** @internal */
    private configService: ConfigService
  ) { }

  ngOnInit(): void {
    this.configService.getConfig()
          .subscribe((cfg) => this.modules = cfg.modules );
  }

}
