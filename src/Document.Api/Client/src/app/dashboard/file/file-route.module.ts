import { NgModule } from "@angular/core";
import { RouterModule, Routes } from "@angular/router";
import { FileListComponent } from "./components/file-list/file-list.component";

const routes: Routes = [
    { path: '', component: FileListComponent},

];

@NgModule({
    exports: [RouterModule],
    imports: [RouterModule.forChild(routes)]
})
export class FileRoutingModule { }
