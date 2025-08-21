def get_student_details():
    """Get student details from user input"""
    students = []
    
    while True:
        try:
            num_students = int(input("Enter the no of student details to be created: "))
            
            # Check if number of students is valid
            if num_students <= 0:
                print("Invalid Input")
                if students:
                    print("Current student details:")
                    for student in students:
                        print(f"{{'Name': '{student['Name']}', 'Age': {student['Age']}}}")
                return students
            
            # Get details for each student
            for i in range(num_students):
                name = input("Name : ")
                age = int(input("Age : "))
                
                # Validate age
                if age <= 10 or age >= 80:
                    print("Invalid Input")
                    if students:
                        print("Current student details:")
                        for student in students:
                            print(f"{{'Name': '{student['Name']}', 'Age': {student['Age']}}}")
                    return students
                
                # Add student to list
                student_dict = {'Name': name, 'Age': age}
                students.append(student_dict)
            
            # Ask if user wants to add more students
            while True:
                choice = input("Do you want to add more students' details to the list of dictionaries? If yes, press 1, else press 0: ")
                
                if choice == '0':
                    return students
                elif choice == '1':
                    break
                else:
                    print("Invalid Input")
                    print("Here's the list of student details :")
                    for student in students:
                        print(f"{{'Name': '{student['Name']}', 'Age': {student['Age']}}}")
                    
                    print("\nHere's the list of student details sorted with respect to name :")
                    sorted_students = sorted(students, key=lambda x: x['Name'])
                    for student in sorted_students:
                        print(f"{{'Name': '{student['Name']}', 'Age': {student['Age']}}}")
                    return students
                    
        except ValueError:
            print("Invalid Input")
            if students:
                print("Current student details:")
                for student in students:
                    print(f"{{'Name': '{student['Name']}', 'Age': {student['Age']}}}")
            return students

def main():
    """Main function to run the student management program"""
    print("Student Management System")
    print("=" * 30)
    
    # Get student details
    students = get_student_details()
    
    if students:
        # Display original list
        print("\nHere's the list of student details :")
        for student in students:
            print(f"{{'Name': '{student['Name']}', 'Age': {student['Age']}}}")
        
        # Display sorted list
        print("\nHere's the list of student details sorted with respect to name :")
        sorted_students = sorted(students, key=lambda x: x['Name'])
        for student in sorted_students:
            print(f"{{'Name': '{student['Name']}', 'Age': {student['Age']}}}")
    else:
        print("No student details to display.")

if __name__ == "__main__":
    main()