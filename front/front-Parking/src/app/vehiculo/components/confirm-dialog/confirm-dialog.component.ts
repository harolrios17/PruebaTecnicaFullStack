import { CommonModule } from '@angular/common';
import { Component, inject } from '@angular/core';

//Material
import { MatButtonModule } from '@angular/material/button';
import { MatDialogModule, MatDialogRef } from '@angular/material/dialog';

@Component({
  selector: 'app-confirm-dialog',
  standalone: true,
  imports: [CommonModule, MatDialogModule, MatButtonModule],
  templateUrl: './confirm-dialog.component.html',
  styleUrl: './confirm-dialog.component.css'
})
export class ConfirmDialogComponent {

  readonly dialogRef = inject(MatDialogRef<ConfirmDialogComponent>);

  onCancel():void {
    this.dialogRef.close(false);
  }

  onConfirm():void {
    this.dialogRef.close(true);
  }

}
