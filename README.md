// Jared Ly, Xavier Hopkins, Joseph Poncini

// 5/28/2024

// Shortalk - Back End Side

// Create a backend to our Full Stack Project to handle User Login, Friend Listings, Game Lobbying, and real time multiplayer interactions

// 

// Peer Review: (Name) (Data) - Description


//------------------------------------ To Do ---------------------------------------------


Requirements
    - able to login
    - create an account

    Pages
        - Dashboard

        - Game Lobby
    

    Controllers / folder
        - UserController / file
            - Login user / endpoint
            - Create user /  endpoint
            - Delete user /  enpoint
            - Update use / endpoint

        - LobbyController / file
            - Create lobby
            - Join Lobby
            - Edit Lobby
            - Delete Lobby
    
    Services / folder
        - Context / folder
            -DataContext /  file
        - UserService / file
            - Login user / function
            - Create user /  function
            - Delete user /  function
            - Update use / function
            - GetUserByUsername / function
        - LobbyService / file
            - Create lobby / function
            - Join Lobby / function
            - Edit Lobby / function
            - Delete Lobby / function

            - Join Lobby by ID
            - Join Lobby by LobbyName

        - PasswordService
            - Hash Password
            - Verify HashPassword

    Models / folder
        - UserModel / file
            - int ID
            - string Username
            - string Salt
            - string Hash


        - LobbyModel (model for each lobby room)
            - int ID
            - string LobbyName


            - DTO / folder
                - LoginDTO / file
                    - string Username
                    - string Password
                - CreateAccountDTO / file
                    - int ID = 0
                    - string Username
                    - string Password
                - PasswordDTO / file
                    - string Salt
                    - string Hash



username: ShortalkLogin
password: ShortalkPassword2357!
