import { Component, OnInit } from '@angular/core';
import { GroundService } from '../../../../services/ground.service'; // Import the GroundService
import { CommonModule } from '@angular/common';

@Component({
  selector: 'app-ground-list',
  templateUrl: './ground-list.component.html',
  styleUrls: ['./ground-list.component.css'],
  standalone: true,
  imports: [CommonModule]  // Import the necessary modules (e.g., CommonModule)
})
export class GroundListComponent implements OnInit {
  grounds: any[] = [];
  errorMessage: string | null = null;
  isLoading: boolean = false;

  constructor(private groundService: GroundService) {}

  ngOnInit(): void {
    this.isLoading = true;
    this.groundService.getAvailableGrounds().subscribe({
      next: (response: any) => {
        this.grounds = response.$values;  // Assign the fetched grounds data
        this.isLoading = false;
      },
      error: (err) => {
        this.errorMessage = 'Failed to load grounds. Please try again later.';
        this.isLoading = false;
      }
    });
  }
}
