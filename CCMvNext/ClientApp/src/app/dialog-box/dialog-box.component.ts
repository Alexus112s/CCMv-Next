//dialog-box.component.ts
import { Component, Inject, Optional, ViewEncapsulation } from '@angular/core';
import { MatDialogRef, MAT_DIALOG_DATA } from '@angular/material/dialog';
import { ICookieConsentRecord } from '../../generated/models';

export interface IDialogData {
  data?: ICookieConsentRecord,
  action: string
}

@Component({
  selector: 'app-dialog-box',
  templateUrl: './dialog-box.component.html',
  styleUrls: ['./dialog-box.component.less'],
  encapsulation: ViewEncapsulation.None
})
export class DialogBoxComponent {
  action: string;
  local_data: ICookieConsentRecord = {
    clientId: Math.random().toString(),
    date: new Date(),
    ip: '127.0.0.1',
    isAccepted: false,
  };

  constructor(
    public dialogRef: MatDialogRef<DialogBoxComponent>,

    @Optional() @Inject(MAT_DIALOG_DATA) public data: IDialogData) {
    if (data.data) {
      this.local_data = data.data;
    }
    this.action = data.action;
  }

  doAction() {
    this.dialogRef.close({ event: this.action, data: this.local_data });
  }

  closeDialog() {
    this.dialogRef.close({ event: 'Cancel' });
  }

}
