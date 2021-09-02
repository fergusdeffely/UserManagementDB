# UserManagementDB
Database access for User Management EDP project


## User Management Database structure:
==================================

DABASE NAME: usermanagement

The usermanagment database consists of a single table.

TABLE: users
COLUMNS 	email           VARCHAR(100) NOT NULL,
            name            VARCHAR(100) NOT NULL
            phone           VARCHAR(50) NOT NULL
            administrator   INT NOT NULL
            password        VARCHAR(50)
            org_group       VARCHAR(100)
            image           VARCHAR(200)
PRIMARY KEY: email


   
## UserManagmentDB VisualStudio project:
====================================
   
The UserManagmentDB VisualStudio project contains the following classes:

### MainForm
---------

 Defines the applications main form and shows examples of usage for the DBConnection, UserDB and User classes.
 


### User
---------

 Properties:
  - Email           (string)
  - Name            (string)
  - Phone           (string)
  - IsAdmin         (bool)
  - Password        (string)
  - PasswordHidden  (string)
  - Group           (string)
  - Image           (string)
  
  Constructor:
  
    public User(string email, string name, string phone, bool admin, string password, string group, string image)
 
  Constructor sample usage:
    
    User user = new User("aaaa@aaa.com", "aaaa", "12345", false, "aaaa", "aaaa", string.Empty);
    
  Public Methods:
  
   - string GetString()
     ------------------
     Returns string representation of the current User instance
     


### UsersDB
---------

 Properties:
    - None
 
 Constructor:
    - Default constructor
      That is:
        UsersDB usersDB = new UsersDB();

 Public Methods:
 
    - List<User> GetUsers(DBConnection db)
      ------------------------------------
      
      Gets a list of all records in the users table.
      
      Params:
        - db: an open connection to a MySql (usermanagement) database
      Returns:
        - A list of User objects representing the records contain in the recordset.
        
      See MainForm.buttonQuery_Click for sample usage
        
    
    - bool Insert(DBConnection db, User user)
      ---------------------------------------
    
      Inserts a new record in the users table.
      
      Params:
        - db: an open connection to a MySql (usermanagement) database
        - user: an instance of User containing the details of the user record to be inserted.
      
      Returns:
        - True if successful, false otherwise.
      
      See MainForm.buttonInsert_Click for sample usage      
    
    
    - public bool Update(DBConnection db, string email, User user)
      ------------------------------------------------------------
    
      Updates an existing record in the users table.
      
      Params:
        - db: an open connection to a MySql (usermanagement) database
        - email: the email address of the existing user record
        - user: an instance of User containing the new details of the user record to be udated.
      
      Returns:
        - True if successful, false otherwise.      
        
      See MainForm.buttonUpdate_Click for sample usage
        
    
    - bool Delete(DBConnection db, string email)
      ------------------------------------------
    
      Deletes a record from the users table.
      
      Params:
        - db: an open connection to a MySql (usermanagement) database
        - email: the email address of the user record to be deleted
    
      Returns:
        - True if successful, false otherwise.      

      See MainForm.buttonDelete_Click for sample usage
        


### DBConnection
--------------
 
 Properties:
  - Connection      (MySqlConnection)
    Represents a connection to a MySql database
    
 Constructor:
    public DBConnection(string server, string database, string uid, string password)
    
 Constructor sample usage:
 
    DBConnection db = new DBConnection("localhost", "usermanagement", "csharp", "password");
    
  Public Methods:
  
    - bool OpenConnection()
      ---------------------
      Attempts to open a connection the the database using the current server, database and user details. 
      
      Returns true if successful, false otherwise.
      
      See: MainForm.buttonConnect_Click for sample usage.
      
      
    - bool CloseConnection()
      ----------------------
      Closes a previously opened connection. 
      
      Returns true if successful, false otherwise.
      
      See: MainForm.MainForm_FormClosing for sample usage.
      
      
    - MySqlDataAdapter ExecuteTableQuery(string sql)
      ---------------------------------------------
      Queries a table as specified in the sql query string parameter. 

      Returns the recordset received from the database as a MySqlDataAdapter.