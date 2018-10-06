import { AppModule } from './config.module.model';

export interface AppConfig {
  appTitle: string;
  modules: AppModule[];
}
