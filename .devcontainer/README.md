# Devcontainer with Claude Code Integration

This devcontainer configuration provides a development environment with [Claude Code](https://claude.ai/code) pre-installed and configured for persistent authentication across container rebuilds.

## Features

- Claude Code CLI automatically installed on container creation
- Persistent Claude authentication via Docker volume (survives rebuilds)
- `ULTRAINSTINCT` alias for running Claude with auto-approved permissions
- Project dependencies restored automatically

## How It Works

### Persistent Authentication

The key challenge with Claude Code in devcontainers is that authentication is lost when the container is rebuilt. This setup solves that by mounting a Docker volume to store Claude's config:

```json
"mounts": [
  "source=my-project-claude-config,target=/home/vscode/.claude,type=volume"
]
```

The volume name (`my-project-claude-config`) can be anything unique. Using a project-specific name allows different auth contexts per project if needed.

### Post-Create Script

The `post-create.sh` script runs after the container is created and handles:

1. **Volume ownership fix** - Docker volumes are created as root, so we chown to the container user
2. **Claude Code installation** - Downloads and installs the CLI
3. **Project setup** - Restores dependencies (customize for your stack)
4. **Shell aliases** - Adds convenience aliases like `ULTRAINSTINCT`

## Adapting for Your Project

### 1. Copy the files

Copy these files to your project's `.devcontainer/` folder:
- `devcontainer.json`
- `post-create.sh`
- `README.md` (optional)

### 2. Update devcontainer.json

```json
{
  "name": "Your Project Name",

  // Change to your preferred base image
  "image": "mcr.microsoft.com/devcontainers/dotnet:1-8.0",

  // Update ports for your application
  "forwardPorts": [3000],

  // Use a unique volume name for your project
  "mounts": [
    "source=YOUR-PROJECT-claude-config,target=/home/vscode/.claude,type=volume"
  ],

  "postCreateCommand": "bash .devcontainer/post-create.sh",

  // Add your preferred extensions
  "customizations": {
    "vscode": {
      "extensions": [
        // your extensions here
      ]
    }
  },

  "remoteUser": "vscode"
}
```

### 3. Customize post-create.sh

```bash
#!/bin/bash
set -e

# === CLAUDE CODE SETUP (keep as-is) ===

# Fix ownership of the Claude config volume mount
sudo chown -R vscode:vscode /home/vscode/.claude

# Install Claude Code CLI
curl -fsSL https://claude.ai/install.sh | bash

# === PROJECT-SPECIFIC SETUP (customize below) ===

# Examples for different stacks:

# .NET
dotnet restore

# Node.js
# npm install

# Python
# pip install -r requirements.txt

# Rust
# cargo build

# === ALIASES ===

# ULTRAINSTINCT: Claude with all permissions auto-approved
echo 'alias ULTRAINSTINCT="ENABLE_BACKGROUND_TASKS=1 FORCE_AUTO_BACKGROUND_TASKS=1 claude --dangerously-skip-permissions"' >> ~/.bashrc
```

## Usage

### First Time Setup

1. Open the project in VS Code
2. When prompted, click "Reopen in Container" (or run `Dev Containers: Reopen in Container`)
3. Wait for the container to build and post-create script to complete
4. Run `claude` and authenticate when prompted
5. Your authentication is now persisted in the Docker volume

### Daily Usage

- `claude` - Start Claude Code normally
- `ULTRAINSTINCT` - Start Claude with auto-approved permissions (use with caution)

### After Container Rebuild

Your Claude authentication persists automatically. Just run `claude` and you're ready to go.

## The `ULTRAINSTINCT` Alias

The `ULTRAINSTINCT` alias runs Claude Code with all safety prompts disabled:

```bash
alias ULTRAINSTINCT="ENABLE_BACKGROUND_TASKS=1 FORCE_AUTO_BACKGROUND_TASKS=1 claude --dangerously-skip-permissions"
```

| Flag | Purpose |
|------|---------|
| `ENABLE_BACKGROUND_TASKS=1` | Allows background task execution |
| `FORCE_AUTO_BACKGROUND_TASKS=1` | Auto-approves background tasks without prompting |
| `--dangerously-skip-permissions` | Skips all file/command permission prompts |

**Use responsibly.** This mode is convenient for trusted projects but removes guardrails that prevent unintended changes.

## Troubleshooting

### "Permission denied" errors on Claude config

The volume might have wrong ownership. Run:
```bash
sudo chown -R vscode:vscode /home/vscode/.claude
```

### Claude not found after rebuild

The post-create script may have failed. Check the terminal output or run manually:
```bash
curl -fsSL https://claude.ai/install.sh | bash
```

### Need to re-authenticate

If your token expires or you need to switch accounts:
```bash
claude logout
claude login
```

### Reset everything

To completely reset Claude's config for this project, delete the Docker volume:
```bash
docker volume rm YOUR-PROJECT-claude-config
```
Then rebuild the container.
