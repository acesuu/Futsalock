import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormGroup, Validators, ReactiveFormsModule } from '@angular/forms'; 
import { GroundService } from '../../../../services/ground.service';
import { BookingService } from '../../../../services/booking.service';
import { CommonModule } from '@angular/common';

@Component({
  selector: 'app-ground-list',
  templateUrl: './ground-list.component.html',
  styleUrls: ['./ground-list.component.css'],
  standalone: true,
  imports: [CommonModule, ReactiveFormsModule] 
})
export class GroundListComponent implements OnInit {
  grounds: any[] = [];
  isModalOpen: boolean = false;
  selectedGroundId: number | null = null;
  selectedHourlyRate: number | null = null;
  bookingForm: FormGroup;
  calculatedTotalPrice: number = 0; 

  constructor(
    private groundService: GroundService,
    private bookingService: BookingService,
    private formBuilder: FormBuilder
  ) {
    this.bookingForm = this.formBuilder.group({
      startTime: ['', Validators.required],
      endTime: ['', Validators.required],
    });
  }

  ngOnInit(): void {
    this.groundService.getAvailableGrounds().subscribe({
      next: (response: any) => {
        this.grounds = response.$values;
      },
      error: (err) => {
        console.error('Error loading grounds', err);
      }
    });
  }

  openModal(groundId: number, hourlyRate: number): void {
    this.isModalOpen = true;
    this.selectedGroundId = groundId;
    this.selectedHourlyRate = hourlyRate;
  }

  closeModal(): void {
    this.isModalOpen = false;
    this.selectedGroundId = null;
    this.selectedHourlyRate = null;
    this.calculatedTotalPrice = 0;
    this.bookingForm.reset();
  }

  calculateTotalPrice(): void {
    const startTime = this.bookingForm.get('startTime')?.value;
    const endTime = this.bookingForm.get('endTime')?.value;

    if (startTime && endTime && this.selectedHourlyRate) {
      const start = new Date(startTime);
      const end = new Date(endTime);
      const diffInHours = (end.getTime() - start.getTime()) / (1000 * 60 * 60); 

      if (diffInHours > 0) {
        this.calculatedTotalPrice = diffInHours * this.selectedHourlyRate; 
      } else {
        this.calculatedTotalPrice = 0;
      }
    }
  }

  onSubmit(): void {
    if (this.bookingForm.valid) {
      const bookingData = {
        groundId: this.selectedGroundId,
        startTime: this.bookingForm.get('startTime')?.value,
        endTime: this.bookingForm.get('endTime')?.value,
        totalPrice: this.calculatedTotalPrice 
      };

      this.bookingService.createBooking(bookingData).subscribe({
        next: (response) => {
          console.log('Booking successful', response);
          this.closeModal();
        },
        error: (err) => {
          console.error('Booking failed', err);
        }
      });
    }
  }
}
