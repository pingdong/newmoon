[Back](../../angular-frontend.md)

## Installing

npm install @ngrx/schematics --save-dev
ng config cli.defaultCollection @ngrx/schematics

npm install @ngrx/store @ngrx/effects @ngrx/store-devtools @ngrx/router-store --save

ng generate store State --root --statePath store/reducers --module app.module.ts

ng generate effect store/App --group --root --spec false --module app.module