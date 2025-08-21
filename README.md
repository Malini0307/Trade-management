# Student Management System

A Python program that demonstrates working with Collections - Lists and Dictionaries to manage student information.

## Description

This program allows school administrators to:
- Collect student names and ages
- Store them in a list of dictionaries
- Add multiple batches of student details
- View the data in both original and sorted order (by name)

## Features

- **Input Validation**: Checks for valid number of students and age ranges
- **Flexible Input**: Allows adding multiple batches of students
- **Data Storage**: Uses lists to store dictionaries containing student information
- **Sorting**: Automatically sorts students by name for display
- **Error Handling**: Gracefully handles invalid inputs

## Requirements

- Python 3.x
- No external dependencies required

## How to Run

1. **Interactive Mode** (for actual data entry):
   ```bash
   python3 student_management.py
   ```

2. **Test Mode** (to see sample output):
   ```bash
   python3 test_student_management.py
   ```

## Program Flow

1. Enter the number of students to add
2. For each student, provide:
   - Name
   - Age (must be between 11-79)
3. Choose whether to add more students:
   - Press `1` to continue adding
   - Press `0` to finish and view results
4. View the final list in both original and sorted order

## Input Validation Rules

- **Number of students**: Must be positive (> 0)
- **Age**: Must be between 11 and 79 (inclusive)
- **Choice**: Must be either `0` or `1`

## Sample Output

```
Enter the no of student details to be created: 2
Name : Angelina
Age : 22
Name : Brad
Age : 25
Do you want to add more students' details to the list of dictionaries? If yes, press 1, else press 0: 1
Enter the no of student details to be created: 2
Name : Ann
Age : 20
Name : Joel
Age : 22
Do you want to add more students' details to the list of dictionaries? If yes, press 1, else press 0: 0

Here's the list of student details :
{'Name': 'Angelina', 'Age': 22}
{'Name': 'Brad', 'Age': 25}
{'Name': 'Ann', 'Age': 20}
{'Name': 'Joel', 'Age': 22}

Here's the list of student details sorted with respect to name :
{'Name': 'Angelina', 'Age': 22}
{'Name': 'Ann', 'Age': 20}
{'Name': 'Brad', 'Age': 25}
{'Name': 'Joel', 'Age': 22}
```

## Data Structure

The program uses:
- **List**: `students = []` to store multiple student records
- **Dictionary**: `{'Name': 'Student Name', 'Age': student_age}` for each student
- **Sorting**: `sorted(students, key=lambda x: x['Name'])` to sort by name

## Error Handling

- Invalid number of students → "Invalid Input"
- Invalid age (≤10 or ≥80) → "Invalid Input"
- Invalid choice (not 0 or 1) → "Invalid Input"
- All error cases display existing student data if available

## Learning Objectives

This program demonstrates:
1. Working with Python lists and dictionaries
2. Input validation and error handling
3. Data sorting and manipulation
4. User interaction and program flow control
5. String formatting and output display