#!/bin/bash
set -e

# Ensure essential directories exist (empty home volume on first run)
mkdir -p /home/vscode/.claude
mkdir -p /home/vscode/.local/bin

# Ensure .bashrc exists if it doesn't already
if [ ! -f /home/vscode/.bashrc ]; then
    cp /etc/skel/.bashrc /home/vscode/.bashrc 2>/dev/null || touch /home/vscode/.bashrc
fi

# Fix ownership of the home directory
sudo chown -R vscode:vscode /home/vscode

# Install Claude Code CLI
curl -fsSL https://claude.ai/install.sh | bash

# If API key is set, no login needed. Otherwise, user will need to authenticate via OAuth.
if [ -n "$ANTHROPIC_API_KEY" ]; then
  echo "ANTHROPIC_API_KEY detected - Claude Code will use API key authentication"
else
  echo "No API key set - Claude Code will use OAuth authentication (may require re-auth on container rebuild)"
fi

# Restore .NET dependencies
dotnet restore

# Add 'ULTRAINSTINCT' alias for Claude Code with auto-permissions
echo 'alias ULTRAINSTINCT="ENABLE_BACKGROUND_TASKS=1 FORCE_AUTO_BACKGROUND_TASKS=1 claude --dangerously-skip-permissions"' >> ~/.bashrc