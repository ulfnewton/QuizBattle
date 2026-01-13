# Git CLI — Tips & Tricks för YH‑studenter 👩‍💻👨‍💻

> **Mål:** Hjälpa dig att bli trygg och effektiv med Git via kommandoraden.  
> **Språk:** Svenska (kommandon och tekniska termer på engelska).

***

## Innehåll

*   snabbstart
*   dagligt-arbetsflöde-team
*   vanliga-kommandon-cheatsheet
*   branching-main-feature-branch-release
*   synka--samarbeta-pull-fetch-merge-rebase
*   historik-logg--navigering
*   ångra--återställ-säkert
*   stash-parkera-halvfärdigt-arbete
*   diff--visuella-verktyg
*   konfiguration--kvalitet
*   alias-som-sparar-tid
*   ssh-nycklar--cred-helpers
*   gitignore--håll-repo-rent
*   commit-meddelanden-som-hjälper-teamet
*   avancerat-cherry-pick-bisect-reflog
*   mini-övningar-cli-fokus

***

## Snabbstart

```bash
# 1) Klona ett repo
git clone <url>

# 2) Gå in i mappen
cd <repo>

# 3) Skapa en ny branch för din uppgift/feature
git switch -c feature/min-uppgift

# 4) Jobba, se status och stage:a förändringar
git status
git add <fil>            # eller: git add .

# 5) Commita med tydligt meddelande
git commit -m "Lägg till X; förbättra Y"

# 6) Skicka upp din branch
git push -u origin feature/min-uppgift
```

> **Tips:** Använd `git switch` och `git restore` (modernare än `checkout`), finns i Git ≥ 2.23.

***

## Dagligt arbetsflöde (team)

1.  **Uppdatera din arbetsbranch från `main`:**
    ```bash
    git fetch origin
    git switch main
    git pull --ff-only      # säker fast-forward
    git switch feature/min-uppgift
    git rebase main         # eller: git merge main
    ```
2.  **Jobba & commita ofta:** små, atomiska commits.
3.  **Push:a och skapa Pull Request:** be om review och kör CI.
4.  **Håll din PR liten:** lättare att granska och att integrera.

***

## Vanliga kommandon (cheatsheet)

```bash
# Status & vad som ändrats
git status -sb

# Stage/unstage
git add <fil>          # stage
git restore --staged <fil>  # unstage

# Skapa commit
git commit -m "Kort, imperativt meddelande"

# Se lokala brancher
git branch

# Byt/skap branch
git switch <branch>
git switch -c <ny-branch>

# Utskrifter av logg
git log --oneline --graph --decorate --all

# Synka mot fjärr
git fetch origin
git pull --ff-only          # hämta + integrera
git push                    # skicka upp

# Merge & rebase
git merge <branch>
git rebase <branch>

# Ta bort filer från versionering (utan att radera lokalt)
git rm --cached <fil>
```

***

## Branching: `main`, feature‑branch, release

*   **`main`**: Alltid grön och utgivningsbar.
*   **Feature‑branch**: En branch per uppgift/issue. Döp beskrivande: `feature/login-form`, `bugfix/null-check`.
*   **Release/Hotfix**: När ni släpper versioner eller akut fixar produktionsbuggar.

> **Tips:** Skapa branch från **senaste `main`** för att minska konflikter senare.

***

## Synka & samarbeta: `pull`, `fetch`, `merge`, `rebase`

*   `git fetch` hämtar **utan** att ändra din arbetsbranch.
*   `git pull` = `fetch` + integrera (merge eller rebase beroende på inställning).
*   **Merge**: skapar merge‑commit; bra när ni vill bevara historik som den hände.
*   **Rebase**: “spola” om dina commits ovanpå målet; renare historia, men **undvik rebase av redan pushade, delade brancher** (kan ställa till det för andra).

```bash
# Ren uppdatering från main
git fetch origin
git rebase origin/main
# Om konflikt: lös, sedan
git rebase --continue
# Avbryt vid behov
git rebase --abort
```

***

## Historik, logg & navigering

```bash
# Fin översikt över historik
git log --oneline --graph --decorate --all

# Visa skillnader
git diff                     # arbetskatalog vs index (staging)
git diff --staged            # index vs HEAD

# Visa exakt vad som ändrats i fil
git diff <fil>

# Sök i historik
git log -S "sökterm"
git log -- <sökväg/fil>
```

***

## Ångra & återställ säkert

> **Grundregel:** Ångra i rätt “lager”: arbetskatalog, staging (index), eller commit‑nivå.

```bash
# 1) Ångra ändringar i en fil (ej stage:ad)
git restore <fil>

# 2) Ta bort fil från staging, behåll ändringarna
git restore --staged <fil>

# 3) Ändra senaste commit‑meddelandet (inget nytt innehåll)
git commit --amend -m "Bättre meddelande"

# 4) Skapa ny commit som ångrar en annan commit
git revert <commitSHA>

# 5) Reset (var försiktig!)
git reset --soft HEAD~1   # behåll ändringar i staging
git reset --mixed HEAD~1  # behåll ändringar i arbetskatalog (default)
git reset --hard HEAD~1   # släng ändringar (FARLIGT)
```

> **Felsäkerhet:** `git revert` är säkert i delade brancher.  
> **Varning:** `reset --hard` och `push --force` kan orsaka dataförlust—använd med stor omsorg (helst i egna brancher).

***

## Stash: parkera halvfärdigt arbete

```bash
# Lägg undan aktuella ändringar snabbt
git stash push -m "WIP: formulärvalidering"

# Visa stash‑lista
git stash list

# Plocka tillbaka (behåll kopia i stash)
git stash apply <stash@{n}>

# Plocka tillbaka och ta bort ur stash
git stash pop

# Rensa specifik eller all stash
git stash drop <stash@{n}>
git stash clear
```

***

## Diff & visuella verktyg

```bash
# För bättre diff-upplevelse
git config --global color.ui true
git config --global core.pager "less -FRSX"  # snabbare navigering

# Konfigurera extern difftool (exempel: code)
git config --global diff.tool vscode
git config --global difftool.vscode.cmd "code --wait --diff $LOCAL $REMOTE"

# Kör difftool
git difftool
```

***

## Konfiguration & kvalitet

```bash
# Basinfo
git config --global user.name "Ditt Namn"
git config --global user.email "din@mail"

# Editor (VS Code)
git config --global core.editor "code --wait"

# Automatisk radavslut
git config --global core.autocrlf input   # mac/linux
# Windows (om ni måste)
git config --global core.autocrlf true

# Pull-strategi (mer säkert)
git config --global pull.ff only          # undvik oavsiktliga merges

# Visa kort status
git config --global advice.statusHints false
```

***

## Alias som sparar tid

Lägg i `~/.gitconfig`:

```ini
[alias]
  st = status -sb
  co = switch
  cob = switch -c
  br = branch
  lg = log --oneline --graph --decorate --all
  aa = add -A
  cm = commit -m
  amend = commit --amend --no-edit
  df = diff
  dff = diff --name-only
  stg = restore --staged
  rv = revert
  rb = rebase
  fco = fetch origin
  pf = push --force-with-lease
```

> **Tips:** `--force-with-lease` är säkrare än `--force`.

***

## SSH‑nycklar & cred‑helpers

```bash
# Skapa SSH-nyckel (byta e-post)
ssh-keygen -t ed25519 -C "din@mail"

# Starta agent & lägg till nyckel
eval "$(ssh-agent -s)"
ssh-add ~/.ssh/id_ed25519

# Visa publik nyckel och lägg upp i GitHub/GitLab
cat ~/.ssh/id_ed25519.pub
```

Credential helpers:

```bash
# Spara inloggning säkert (OS-beroende)
git config --global credential.helper manager-core   # Windows
git config --global credential.helper osxkeychain    # macOS
git config --global credential.helper cache          # Linux (temporärt)
```

***

## .gitignore — håll repo rent

Exempel:

```gitignore
# Node.js
node_modules/
dist/
.env
# Python
__pycache__/
*.pyc
# IDE
.vscode/
.idea/
# OS
.DS_Store
Thumbs.db
```

> **Tips:** Använd mallar från gitignore.io eller GitHub **global** ignore för din editor/OS.

***

## Commit‑meddelanden som hjälper teamet

**Format (kort & tydligt):**

*   **Titel (max ~72 tecken)**, imperativ: “Lägg till validering för e‑post”
*   **Body (om behövs):** varför/följder, referens till issue/ticket.

Exempel:

```text
Bygg: lägg till CI-jobb för enhetstester

- Kör npm test på push/PR
- Snabbare feedback för teamet
Refs: #123
```

> **Tips:** Commita **logiska, atomiska** förändringar. Separera refaktorering från funktionell ändring.

***

## Avancerat: cherry‑pick, bisect, reflog

```bash
# Cherry-pick: ta en commit från annan branch
git cherry-pick <commitSHA>

# Bisect: hitta commit som introducerade en bug
git bisect start
git bisect bad                    # nuvarande är dålig
git bisect good <commitSHA>       # känd bra commit
# Testa, markera good/bad tills Git pekar ut skyldig commit
git bisect reset

# Reflog: räddningslina om du "tappade" något
git reflog         # visar HEAD-historik
git switch -d <reflog-entry>  # eller reset till tidigare läge
```

***

## Mini‑övningar (CLI‑fokus)

1.  **Skapa och pusha feature‑branch**
    ```bash
    git switch -c feature/hello-cli
    echo "Hello" > hello.txt
    git add hello.txt
    git commit -m "Lägg till hello.txt"
    git push -u origin feature/hello-cli
    ```

2.  **Rebase mot ny `main`**
    ```bash
    git fetch origin
    git switch feature/hello-cli
    git rebase origin/main
    ```

3.  **Stash och pop**
    ```bash
    echo "WIP" >> hello.txt
    git stash push -m "WIP: hello"
    git pull --ff-only
    git stash pop
    ```

4.  **Ångra säkert med `revert`**
    ```bash
    git log --oneline
    git revert <senaste-commitSHA>
    ```

5.  **Logg‑navigering & diff**
    ```bash
    git log --oneline --graph --decorate --all
    git diff --staged
    ```

***

### Små guldkorn

*   `git status -sb` ger kompakt status med branchinfo.
*   `git add -p` för att stage:a **delar** av en fil (perfekt för tydliga commits).
*   `git restore --source=<commitSHA> <fil>` plockar en filversion från historik.
*   `git push --force-with-lease` om du **måste** uppdatera remote efter rebase.
*   Håll PR:er små (≤ ~300 rader netto) → snabbare review, färre konflikter.
