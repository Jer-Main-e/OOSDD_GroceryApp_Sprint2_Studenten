# GroceryApp sprint2 

## Docentversie  
In deze versie zijn de wijzigingen doorgevoerd en is de code compleet.  

## Studentversie:  
### UC04 Kiezen kleur boodschappenlijst  
Is compleet.

### UC05 Product op boodschappenlijst plaatsen:   
- `GetAvailableProducts()`  
	De header van de functie bestaat maar de inhoud niet.  
	Zorg dat je een lijst maakt met de beschikbare producten (dit zijn producten waarvan nog voorraad bestaat en die niet al op de boodschappenlijst staat).  
- `AddProduct()`   
	Zorg dat het gekozen beschikbare product op de boodschappenlijst komt (door middel van de GroceryListItemsService).  

### UC06 Inloggen  
Een collega is ziek maar heeft al een deel van de inlogfunctionaliteit gemaakt.  
Dit betreft het Loginscherm (LoginView) met bijbehorend ViewModel (LoginViewModel),  
maar ook al een deel van de authenticatieService (AuthServnn,mnmice in Grocery.Core),  
de clientrepository (ClientRepository in Grocery.Core.Data)  
en de client class (Client in Grocery.Core).  
De opdracht is om zelfstandig de login functionaliteit te laten werken.  

*Stappenplan:*  
1. Begin met de Client class en zorg dat er gebruik wordt gemaakt van Properties.  
2. In de ClienRepository wordt nu steeds een vaste client uit de lijst geretourneerd. Werk dit uit zodat de juiste Client wordt geretourneerd.  
3. Werk de klasse AuthService verder uit, zodat daadwerkelijk de controle op het ingevoerde password wordt uitgevoerd.
4. Zorg dat de LoginView.xaml wordt toegevoegd aan het Grocery.App project in de Views folder (Add ExistingItem). De file bevindt zich al op deze plek, maar wordt nu niet meegecompileerd.  
5. In MauiProgramm class van de Grocery.App staan de registraties van de AuthService en de LoginView in comment --> haal de // weg.  
6. In App.xaml.cs staat /*LoginViewModel viewModel*/ haal hier /* en */ weg, zodat het LoginViewModel beschikbaar komt.  
7. In App.xaml.cs staat //MainPage = new LoginView(viewModel); Haal hier de // weg en zet de regel erboven in commentaar, zodat AppShell wordt uitgeschakeld.  
8. Uncomment de route naar het Login scherm in AppShell.xaml.cs: //Routing.RegisterRoute("Login", typeof(LoginView)); 
 
# Branching Strategy

This project follows the **GitFlow** branching model to ensure organized development and stable releases.

## Branch Types

### Main Branches

- **`main`** - Production-ready code. Only merge from `release` or `hotfix` branches
- **`develop`** - Integration branch for features. Latest development changes for the next release

### Supporting Branches

- **`feature/*`** - New features or enhancements
- **`release/*`** - Prepare new production releases
- **`hotfix/*`** - Quick fixes for production issues

## Workflow

### Feature Development
```
1. Create feature branch from develop
   git checkout develop
   git pull origin develop
   git checkout -b feature/awesome-feature

2. Work on your feature
   git add .
   git commit -m "Add awesome feature"

3. Push and create pull request
   git push origin feature/awesome-feature
   # Create PR: feature/awesome-feature → develop
```

### Release Process
```
1. Create release branch from develop
   git checkout develop
   git pull origin develop
   git checkout -b release/v1.2.0

2. Finalize release (bug fixes, version bumps)
   git add .
   git commit -m "Bump version to 1.2.0"

3. Merge to main and develop
   # PR: release/v1.2.0 → main
   # PR: release/v1.2.0 → develop
   # Tag main: git tag v1.2.0
```

### Hotfix Process
```
1. Create hotfix branch from main
   git checkout main
   git pull origin main
   git checkout -b hotfix/critical-bug-fix

2. Fix the issue
   git add .
   git commit -m "Fix critical bug"

3. Merge to main and develop
   # PR: hotfix/critical-bug-fix → main
   # PR: hotfix/critical-bug-fix → develop
```

## Branch Naming Convention

- Features: `feature/description-of-feature`
- Releases: `release/v1.2.0`
- Hotfixes: `hotfix/description-of-fix`
- Use lowercase with hyphens for readability

## Pull Request Guidelines

- **Target Branch**: Features merge to `develop`, releases/hotfixes merge to `main` and `develop`
- **Review Required**: All pull requests require at least one approval
- **Testing**: Ensure all tests pass before merging
- **Clean History**: Use squash merge for feature branches

## Visual Flow

```
main     ----*----*----*----*----
              \    \    \    \
develop  ------*----*----*----*----
                \        \
feature           *--*--*  \
                          \  \
release                    *--*
                            
hotfix            *--*
                  |  |
main     ----*----*--*----*----
```

## Best Practices

- Keep feature branches small and focused
- Regularly sync with `develop` to avoid conflicts
- Delete merged branches to keep repository clean
- Use descriptive commit messages following conventional commits
- Never force push to `main` or `develop`
- Always create pull requests, never push directly to main branches
