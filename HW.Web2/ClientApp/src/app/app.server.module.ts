import { HTTP_INTERCEPTORS } from '@angular/common/http';
import { NgModule } from '@angular/core';
import { ServerModule, ServerTransferStateModule } from '@angular/platform-server';
//import { ModuleMapLoaderModule } from '@nguniversal/module-map-ngfactory-loader';
import { AppComponent } from './app.component';
import { AppModule } from './app.module';
import { ServerStateInterceptor } from './shared/Interceptors/server-state.interceptor';

@NgModule({
  imports: [AppModule, ServerModule, ServerTransferStateModule /*ModuleMapLoaderModule*/],
  providers: [
    // Add universal-only providers here
    {
      provide: HTTP_INTERCEPTORS,
      useClass: ServerStateInterceptor,
      multi: true
    }
  ],
    bootstrap: [AppComponent]
})
export class AppServerModule { }
