#!/bin/bash
set -e

# Fix ownership of the Claude config volume mount
# Required because Docker volumes are created as root
sudo chown -R vscode:vscode /home/vscode/.claude

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
# ENABLE_BACKGROUND_TASKS: Allows background task execution
# FORCE_AUTO_BACKGROUND_TASKS: Auto-approves background tasks
# --dangerously-skip-permissions: Skips all permission prompts
echo 'alias ULTRAINSTINCT="ENABLE_BACKGROUND_TASKS=1 FORCE_AUTO_BACKGROUND_TASKS=1 claude --dangerously-skip-permissions"' >> ~/.bashrc
