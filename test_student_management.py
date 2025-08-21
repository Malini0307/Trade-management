#!/usr/bin/env python3
"""
Test script for student management program
This simulates the input/output behavior described in the requirements
"""

def test_student_management():
    """Test the student management functionality"""
    
    # Simulate the sample input from the requirements
    print("=== Testing Student Management System ===\n")
    
    # Test case 1: Normal operation
    print("Test Case 1: Normal operation with 2 students")
    students = []
    
    # Add first batch of students
    num_students = 2
    print(f"Enter the no of student details to be created: {num_students}")
    
    # Student 1
    name1, age1 = "Angelina", 22
    print(f"Name : {name1}")
    print(f"Age : {age1}")
    students.append({'Name': name1, 'Age': age1})
    
    # Student 2
    name2, age2 = "Brad", 25
    print(f"Name : {name2}")
    print(f"Age : {age2}")
    students.append({'Name': name2, 'Age': age2})
    
    # Ask to add more
    choice = 1
    print(f"Do you want to add more students' details to the list of dictionaries? If yes, press 1, else press 0: {choice}")
    
    # Add second batch
    num_students2 = 2
    print(f"Enter the no of student details to be created: {num_students2}")
    
    # Student 3
    name3, age3 = "Ann", 20
    print(f"Name : {name3}")
    print(f"Age : {age3}")
    students.append({'Name': name3, 'Age': age3})
    
    # Student 4
    name4, age4 = "Joel", 22
    print(f"Name : {name4}")
    print(f"Age : {age4}")
    students.append({'Name': name4, 'Age': age4})
    
    # Final choice
    choice2 = 0
    print(f"Do you want to add more students' details to the list of dictionaries? If yes, press 1, else press 0: {choice2}")
    
    # Display results
    print("\nHere's the list of student details :")
    for student in students:
        print(f"{{'Name': '{student['Name']}', 'Age': {student['Age']}}}")
    
    print("\nHere's the list of student details sorted with respect to name :")
    sorted_students = sorted(students, key=lambda x: x['Name'])
    for student in sorted_students:
        print(f"{{'Name': '{student['Name']}', 'Age': {student['Age']}}}")
    
    print("\n" + "="*50)
    
    # Test case 2: Invalid input (zero students)
    print("Test Case 2: Invalid input - zero students")
    print("Enter the no of student details to be created: 0")
    print("Invalid Input")
    print("No student details to display.")
    
    print("\n" + "="*50)
    
    # Test case 3: Invalid age
    print("Test Case 3: Invalid age")
    print("Enter the no of student details to be created: 1")
    print("Name : Test")
    print("Age : 5")
    print("Invalid Input")
    print("No student details to display.")
    
    print("\n" + "="*50)
    
    # Test case 4: Invalid choice
    print("Test Case 4: Invalid choice")
    students_test = [{'Name': 'John', 'Age': 25}]
    print("Enter the no of student details to be created: 1")
    print("Name : John")
    print("Age : 25")
    print("Do you want to add more students' details to the list of dictionaries? If yes, press 1, else press 0: 2")
    print("Invalid Input")
    print("Here's the list of student details :")
    for student in students_test:
        print(f"{{'Name': '{student['Name']}', 'Age': {student['Age']}}}")
    
    print("\nHere's the list of student details sorted with respect to name :")
    sorted_test = sorted(students_test, key=lambda x: x['Name'])
    for student in sorted_test:
        print(f"{{'Name': '{student['Name']}', 'Age': {student['Age']}}}")

if __name__ == "__main__":
    test_student_management()