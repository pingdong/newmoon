import { AppModule } from './app.module.model';

export interface AppConfig {
  appTitle: string;
  modules: AppModule[];
}
