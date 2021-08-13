import { Component, OnInit, ViewChild } from '@angular/core';
import { MatDialog } from '@angular/material/dialog';
import { MatPaginator } from '@angular/material/paginator';
import { MatTableDataSource } from '@angular/material/table';
import { CookieConsentsService } from '../../generated/api';
import { ICookieConsentRecord } from '../../generated/models';
import { DialogBoxComponent, IDialogData } from '../dialog-box/dialog-box.component';

@Component({
  selector: 'app-cookie-consent-page',
  templateUrl: './cookie-consent-page.component.html',
  styleUrls: ['./cookie-consent-page.component.less']
})
export class CookieConsentPageComponent implements OnInit {
  displayedColumns: string[] = ['id', 'clientId', 'date', 'ip', 'isAccepted', 'Action'];
  data: ICookieConsentRecord[] = [];
  dataSource = new MatTableDataSource<ICookieConsentRecord>(this.data);

  constructor(private cookieConsentsService: CookieConsentsService, public dialog: MatDialog) { }

  @ViewChild(MatPaginator) paginator?: MatPaginator;

  ngAfterViewInit() {
    if (this.paginator) {
      this.dataSource.paginator = this.paginator;
    }
  }

  async ngOnInit(): Promise<void> {
    const res = await this.cookieConsentsService.get();
    this.data = res;

    this.dataSource.data = this.data;
  }

  openDialog(action: string, obj?: ICookieConsentRecord) {
    const dialogData: IDialogData = {
      data: obj,
      action: action
    };

    const dialogRef = this.dialog.open(DialogBoxComponent, {
      width: '300px',
      data: dialogData
    });

    dialogRef.afterClosed().subscribe(result => {
      if (result.event == 'Add') {
        this.add(result.data);
      } else if (result.event == 'Update') {
        this.update(result.data);
      } else if (result.event == 'Delete') {
        this.delete(result.data);
      }
    });
  }

  async add(consent: ICookieConsentRecord): Promise<void> {
    // ToDo: adjust local time.
    const newRecord = await this.cookieConsentsService.post(consent);

    this.data.push(newRecord);
    this.dataSource.data = this.data;
  }

  async update(consent: ICookieConsentRecord): Promise<void> {
    if (consent.id) {
      // ToDo: adjust local time.
      await this.cookieConsentsService.put(consent.id, consent);
    } else {
      throw new Error("Missing ID");
    }
  }

  async delete(consent: ICookieConsentRecord): Promise<void> {
    if (consent.id) {
      await this.cookieConsentsService.delete(consent.id);

      this.data.splice(this.data.findIndex(x => x.id == consent.id), 1);

      this.dataSource.data = this.data;
    }
  }
}
