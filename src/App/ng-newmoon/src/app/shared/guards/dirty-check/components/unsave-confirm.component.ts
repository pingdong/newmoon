import { Component, Inject, ChangeDetectionStrategy } from '@angular/core';
import { MatDialogRef, MAT_DIALOG_DATA } from '@angular/material';

@Component({
  selector: 'app-unsave-confirm',
  templateUrl: './unsave-confirm.component.html',
  styleUrls: ['./unsave-confirm.component.css'],
  changeDetection: ChangeDetectionStrategy.OnPush
})
export class UnsaveConfirmComponent {

  constructor(
    public dialogRef: MatDialogRef<UnsaveConfirmComponent>,
    @Inject(MAT_DIALOG_DATA) public discard: boolean) {}

  onNoClick(): void {
    this.dialogRef.close(false);
  }

  onYesClick(): void {
    this.dialogRef.close(true);
  }

}
