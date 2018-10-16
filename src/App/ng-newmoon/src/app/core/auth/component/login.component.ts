import { Component, OnInit } from '@angular/core';
import { FormBuilder } from '@angular/forms';
import { Validators } from '@angular/forms';
import { Location } from '@angular/common';

import { AuthService } from '../auth.service';

@Component({
  selector: 'app-login',
  styleUrls: ['./login.component.css'],
  templateUrl: './login.component.html',
})
export class LoginComponent implements OnInit {

  isError: boolean;

  // loginForm = new FormGroup({
  //   username: new FormControl('', Validators.required),
  //   password: new FormControl('', Validators.required),
  // });
  loginForm = this.fb.group({
    username: ['', Validators.required],
    password: ['', Validators.required],
  });

  constructor(
    /** @internal */
    private authService: AuthService,
    private location: Location,
    private fb: FormBuilder
  ) { }

  public ngOnInit(): void {

    this.authService.isLoggedIn$
          .subscribe((isLoggedIn) => {
            this.isError = !isLoggedIn;

            if (isLoggedIn) {
              this.authService.redirect();
            }
          });

    this.isError = false;
  }

  public login(): void {
    const model = this.loginForm.value;

    this.authService.login(model.username, model.password);
  }

  public cancel(): void {
    this.location.back();
  }
}
