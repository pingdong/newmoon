import { Component, OnInit } from '@angular/core';
import { FormBuilder, Validators } from '@angular/forms';
import { Location } from '@angular/common';
import { ActivatedRoute, Router } from '@angular/router';

import { AuthService } from '../services/auth.service';

@Component({
  selector: 'app-login',
  styleUrls: ['./login.component.css'],
  templateUrl: './login.component.html',
})
export class LoginComponent implements OnInit {

  public loginForm = this.fb.group({
    username: ['', Validators.required],
    password: ['', Validators.required],
  });

  public isError: boolean;

  private returnUrl: string;

  constructor(
    /** @internal */
    private authService: AuthService,
    private route: ActivatedRoute,
    private router: Router,
    private location: Location,
    private fb: FormBuilder
  ) { }

  public ngOnInit(): void {
    this.authService.isLoggedIn$
          .subscribe(newState => this.onStateChanged(newState));

    // get return url from route parameters or default to '/'
    this.returnUrl = this.route.snapshot.queryParams['returnUrl'] || '/';

    this.isError = false;
  }

  public login(): void {
    if (this.loginForm.invalid) {
      return;
    }

    const model = this.loginForm.value;
    this.authService.login(model.username, model.password);
  }

  public cancel(): void {
    this.location.back();
  }

  private onStateChanged(isLoggedIn: boolean) {
    this.isError = !isLoggedIn;

    if (isLoggedIn) {
      this.router.navigate([this.returnUrl]);
    }
  }

}
